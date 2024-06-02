using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using EnRoute.Domain.Constants;

namespace EnRoute.API.Controllers
{
    public class PickupCountersController : ODataControllerBase<PickupCounter>
    {
        public PickupCountersController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }

        public async override Task<IActionResult> Delete([FromRoute] Guid key)
        {
            var counterToDeinstall = await AppDbContext.PickupCounters.FirstOrDefaultAsync(c => c.Id == key);

            if (counterToDeinstall == null) 
            {
                return NotFound();
            }

            MakeCounterDeinstallationRequest(counterToDeinstall.Id);

            counterToDeinstall.IsDeleted = true;

            await AppDbContext.SaveChangesAsync();
            return Ok(counterToDeinstall);
        }

        public void MakeCounterDeinstallationRequest(Guid counterId)
        {
            RemoveOrders(counterId);

            var deinstallationRequest = new CounterDeinstallationRequest
            {
                CounterId = counterId
            };

            AppDbContext.CounterDeinstallationRequests.Add(deinstallationRequest);
        }

        private void RemoveOrders(Guid counterId)
        {
            var counter = AppDbContext.PickupCounters
            .Include(c => c.Cells)
            .ThenInclude(co => co.Order)
            .FirstOrDefault(c => c.Id == counterId && !c.IsDeleted);

            if (counter != null)
            {
                if (counter.Cells != null)
                {
                    foreach (var cell in counter.Cells)
                    {
                        if (cell.Order != null)
                        {
                            // Change order status to cancelled
                            cell.Order.Status = OrderStatus.CancelledByShop;
                            //AppDbContext.Orders.Remove(cell.Order);
                        }
                    }
                }

                AppDbContext.SaveChanges();
            }
        }
    }
}
