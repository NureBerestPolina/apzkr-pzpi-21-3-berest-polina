using EnRoute.Domain;
using EnRoute.Domain.Models;
using EnRoute.Infrastructure.Commands;
using EnRoute.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.Strategies
{
    public class CustomerStrategy : IRoleStrategy
    {
        public async Task ExecuteRoleSpecificActionAsync(User user, RegisterCommand command, ApplicationDbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
