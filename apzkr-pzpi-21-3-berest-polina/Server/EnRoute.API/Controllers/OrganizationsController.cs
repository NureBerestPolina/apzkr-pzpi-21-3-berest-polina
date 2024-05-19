using EnRoute.Domain;
using EnRoute.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;

namespace EnRoute.API.Controllers
{
    public class OrganizationsController : ODataControllerBase<Organization>
    {
        public OrganizationsController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }

        public override async Task<IActionResult> Delete([FromODataUri] Guid key)
        {
            var organization = await AppDbContext.Organizations.FindAsync(key);

            if (organization == null)
            {
                return NotFound();
            }

            organization.IsBlocked = !organization.IsBlocked;

            await AppDbContext.SaveChangesAsync();

            return Ok(organization);
        }
    }
}