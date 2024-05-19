using EnRoute.Common.Constants;
using EnRoute.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.Strategies
{
    public class RoleStrategyFactory : IRoleStrategyFactory
    {
        public IRoleStrategy CreateStrategy(string role)
        {
            switch (role.ToLower())
            {
                case UserRoles.OrganizationManager:
                    return new OrganizationManagerStrategy();
                case UserRoles.Customer:
                    return new CustomerStrategy();
                case UserRoles.SystemAdministrator:
                    return new SystemAdministratorStrategy();
                default:
                    throw new ArgumentException($"Role {role} is not supported.", nameof(role));
            }
        }
    }
}
