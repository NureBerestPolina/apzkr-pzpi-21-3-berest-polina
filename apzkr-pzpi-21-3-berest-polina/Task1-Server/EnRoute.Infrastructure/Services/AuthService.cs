using EnRoute.Common.Configuration;
using EnRoute.Common.Constants;
using EnRoute.Domain;
using EnRoute.Domain.Models;
using EnRoute.Infrastructure.Commands;
using EnRoute.Infrastructure.Constants;
using EnRoute.Infrastructure.Extentions;
using EnRoute.Infrastructure.Services.Interfaces;
using EnRoute.Infrastructure.Strategies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;
using System.Security.Claims;

namespace EnRoute.Infrastructure.Services
{
    /// <summary>
    /// Service handling authentication operations.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly JwtSettings jwtSettings;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IRoleStrategyFactory roleStrategyFactory;

        public AuthService(ApplicationDbContext dbContext, UserManager<User> userManager, JwtSettings jwtSettings, IJwtTokenService jwtTokenService, IRoleStrategyFactory roleStrategyFactory)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
            this.jwtTokenService = jwtTokenService;
            this.roleStrategyFactory = roleStrategyFactory;
        }

        /// <summary>
        /// Generates a token for the given user.
        /// </summary>
        /// <param name="user">User entity for which the token is generated.</param>
        /// <returns>A tuple containing the generated token and its refresh token.</returns>
        public async Task<(string Token, string RefreshToken)> GenerateTokenForUserAsync(User user)
        {
            var authClaims = new List<Claim>
        {
            new(JwtClaims.Sub, user.Id.ToString()),
            new(JwtClaims.Email, user.Email!),
            new(JwtClaims.RegisterDate, user.RegisterDate.ToString("O")),
            new(JwtClaims.Name, user.Name)
        };

            var userClaims = await userManager.GetClaimsAsync(user);
            var roleClaims = userClaims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();

            authClaims.Add(new Claim(JwtClaims.Roles, string.Join<string>(",", roleClaims)));

            var token = jwtTokenService.CreateToken(authClaims);
            var refreshToken = jwtTokenService.GenerateRefreshToken();


            var serializedToken = token.SerializeToString();

            var issuedToken = new IssuedToken
            {
                User = user,
                Token = serializedToken,
                RefreshToken = refreshToken,
                RefreshTokenExpirationTime = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenValidityInDays)
            };

            dbContext.IssuedTokens.Add(issuedToken);
            await dbContext.SaveChangesAsync();

            return (serializedToken, refreshToken);
        }

        /// <summary>
        /// Registers a new user with the provided registration information.
        /// </summary>
        /// <param name="command">Registration command containing user information.</param>
        /// <returns>The newly registered user.</returns>
        public async Task<User> RegisterUserAsync(RegisterCommand command)
        {
            var user = new User()
            {
                Email = command.Email,
                UserName = command.Email,
                Name = command.Name
            };

            var result = await userManager.CreateAsync(user, command.Password);

            if (!result.Succeeded)
            {
                throw new Exception($"Unexpected error during user registration: {string.Join(", ", result.Errors)}.");
            }

            var createdUser = await userManager.FindByEmailAsync(user.Email);
            if (createdUser is null)
            {
                throw new Exception("Registered user not found.");
            }

            try
            {
                var claimAssignmentResult = await userManager.AddClaimAsync(createdUser, new Claim(ClaimTypes.Role, command.Role.ToLower()));

                if (!claimAssignmentResult.Succeeded)
                {
                    throw new ArgumentException($"Unexpected error during role claim assignment: {string.Join(", ", claimAssignmentResult.Errors)}.", nameof(command.Role));
                }
                await dbContext.SaveChangesAsync();
                var strategy = roleStrategyFactory.CreateStrategy(command.Role.ToLower());
                await strategy.ExecuteRoleSpecificActionAsync(createdUser, command, dbContext);
               
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                await userManager.DeleteAsync(createdUser);
                throw;
            }

            return createdUser;
        }

        /// <summary>
        /// Refreshes the provided JWT token using the refresh token.
        /// </summary>
        /// <param name="token">Expired JWT token.</param>
        /// <param name="refreshToken">Refresh token.</param>
        /// <returns>A tuple containing the new token and refresh token.</returns>
        public async Task<(string Token, string RefreshToken)> RefreshToken(string token, string refreshToken)
        {
            var principal = this.jwtTokenService.GetPrincipalFromExpiredToken(token);
            if (principal == null)
            {
                throw new AuthenticationException("Invalid access token or refresh token");
            }

            var userId = Guid.Parse(principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);

            var user = await dbContext.Users
                .Include(c => c.IssuedTokens)
                .Where(u => u.Id == userId &&
                            u.IssuedTokens.Any(t =>
                                t.RefreshToken == refreshToken && DateTime.UtcNow <= t.RefreshTokenExpirationTime))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new AuthenticationException("Invalid access token or refresh token");
            }

            var currentToken = user.IssuedTokens.First(c => c.RefreshToken == refreshToken);
            dbContext.IssuedTokens.Remove(currentToken);

            return await GenerateTokenForUserAsync(user);
        }
    }
}
