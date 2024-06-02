using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EnRoute.Infrastructure.Services.Interfaces
{
    public interface IJwtTokenService
    {
        JwtSecurityToken CreateToken(IEnumerable<Claim> authClaims);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
