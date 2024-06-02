using EnRoute.Domain.Models;
using EnRoute.Domain;
using EnRoute.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnRoute.Infrastructure.Services.Interfaces;

namespace EnRoute.Infrastructure.Strategies
{
    public class SystemAdministratorStrategy : IRoleStrategy
    {
        public async Task ExecuteRoleSpecificActionAsync(User user, RegisterCommand command, ApplicationDbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
