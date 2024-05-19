using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Common.Constants
{
    public static class UserRoles
    {
        public static readonly IEnumerable<string> AvailableRoles = new[] { Administrator, SystemAdministrator, OrganizationManager, Customer };

        // Must be lowercase to avoid case sensitivity issues in the future.
        public const string Administrator = "admin";
        public const string SystemAdministrator = "sysadmin";
        public const string OrganizationManager = "organizationmanager";
        public const string Customer = "customer";
    }
}
