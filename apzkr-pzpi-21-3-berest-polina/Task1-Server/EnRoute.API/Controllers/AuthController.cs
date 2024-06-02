using AutoMapper;
using EnRoute.API.Contracts.Auth.Requests;
using EnRoute.API.Contracts.Auth.Responses;
using EnRoute.Common.Configuration;
using EnRoute.Domain;
using EnRoute.Domain.Models;
using EnRoute.Infrastructure.Commands;
using EnRoute.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnRoute.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IMapper mapper;
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly JwtSettings jwtSettings;
        private readonly IConfiguration configuration;

        public AuthController(
            IAuthService authService,
            IMapper mapper,
            ApplicationDbContext dbContext,
            UserManager<User> userManager,
            JwtSettings jwtSettings,
            IConfiguration configuration)
        {
            this.authService = authService;
            this.mapper = mapper;
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.jwtSettings = jwtSettings;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, request.Password))
            {
                return Unauthorized();
            }

            var (token, refreshToken) = await authService.GenerateTokenForUserAsync(user);

            var response = new LoginResponse(token, refreshToken);
            return Ok(response);
        }

        [HttpPost]
        [Route("registerCompany")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterCompany([FromBody] RegisterCompanyRequest request)
        {
            var registerCommand = mapper.Map<RegisterCommand>(request);

            var registeredUser = await authService.RegisterUserAsync(registerCommand);

            var response = new RegisterResponse(registeredUser.Id);
            return Ok(response);
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(typeof(RegisterResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var registerCommand = mapper.Map<RegisterCommand>(request);

            var registeredUser = await authService.RegisterUserAsync(registerCommand);

            var response = new RegisterResponse(registeredUser.Id);
            return Ok(response);
        }

        [HttpPost]
        [Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest refreshTokenRequest)
        {
            var (token, refreshToken) = await authService.RefreshToken(refreshTokenRequest.Token, refreshTokenRequest.RefreshToken);

            var response = new RefreshTokenResponse(token, refreshToken);

            return Ok(response);
        }
    }
}
