using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnRoute.API.Controllers
{
    public class CounterDeinstallationRequestsController : ODataControllerBase<CounterDeinstallationRequest>
    {
        public CounterDeinstallationRequestsController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }

        public async override Task<IActionResult> Delete([FromRoute] Guid key)
        {
            var deinstallRequest = await AppDbContext.CounterDeinstallationRequests.FirstOrDefaultAsync(r => r.Id == key);

            if (deinstallRequest == null)
                return NotFound();

            var counterToDeinstall = await AppDbContext.PickupCounters.Include(c => c.Cells)
                                                                      .FirstOrDefaultAsync(f => f.Id == deinstallRequest.CounterId);

            if (counterToDeinstall == null)
                return NotFound();

            AppDbContext.Cells.RemoveRange(counterToDeinstall.Cells);

            AppDbContext.CounterDeinstallationRequests.Remove(deinstallRequest);
            AppDbContext.PickupCounters.Remove(counterToDeinstall);

            await AppDbContext.SaveChangesAsync();

            return Ok(deinstallRequest);
        }
    }
}
