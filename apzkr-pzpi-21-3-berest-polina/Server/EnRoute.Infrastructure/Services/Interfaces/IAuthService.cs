using EnRoute.Domain.Models;
using EnRoute.Infrastructure.Commands;

namespace EnRoute.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        Task<(string Token, string RefreshToken)> GenerateTokenForUserAsync(User user);

        Task<User> RegisterUserAsync(RegisterCommand command);

        Task<(string Token, string RefreshToken)> RefreshToken(string token, string refreshToken);
    }
}
