using System.IdentityModel.Tokens.Jwt;

namespace EnRoute.Infrastructure.Extentions
{
    public static class JwtSecurityTokenExtensions
    {
        public static string SerializeToString(this JwtSecurityToken token) => new JwtSecurityTokenHandler().WriteToken(token);
    }
}
