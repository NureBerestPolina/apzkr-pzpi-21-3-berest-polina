using EnRoute.Domain.Models;
using EnRoute.Domain;
using EnRoute.Infrastructure.Commands;
using EnRoute.Infrastructure.Services.Interfaces;

namespace EnRoute.Infrastructure.Strategies
{
    public class OrganizationManagerStrategy : IRoleStrategy
    {
        public async Task ExecuteRoleSpecificActionAsync(User user, RegisterCommand command, ApplicationDbContext dbContext)
        {
            var organization = new Organization
            {
                Manager = user,
                Name = command.OrganizationName,
                Description = command.Description
            };
            dbContext.Organizations.Add(organization);
            await dbContext.SaveChangesAsync();
        }
    }
}
