using EnRoute.Domain.Models;
using EnRoute.Domain;
using EnRoute.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.Services.Interfaces
{
    public interface IRoleStrategy
    {
        Task ExecuteRoleSpecificActionAsync(User user, RegisterCommand command, ApplicationDbContext dbContext);
    }
}
