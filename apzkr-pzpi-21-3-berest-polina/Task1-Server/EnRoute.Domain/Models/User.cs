using EnRoute.Domain.Models.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EnRoute.Domain.Models
{
    public class User : IdentityUser<Guid>, IODataEntity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public List<IssuedToken> IssuedTokens { get; set; } = new();
    }
}
