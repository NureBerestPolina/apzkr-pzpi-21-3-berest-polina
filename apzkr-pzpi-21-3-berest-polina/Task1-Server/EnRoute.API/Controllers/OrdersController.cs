using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace EnRoute.API.Controllers
{
    public class OrdersController : ODataControllerBase<Order>
    {
        private readonly IHttpClientFactory httpClientFactory;

        public OrdersController(ApplicationDbContext appDbContext, IHttpClientFactory httpClientFactory) : base(appDbContext)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async override Task<IActionResult> Put([FromRoute] Guid key, [FromBody] Order entity)
        {
            var order = AppDbContext.Orders.Include(o => o.AssignedCell).FirstOrDefault(o => o.Id == key);
            if (order == null)
            {
                return NotFound();
            }

            var counter = AppDbContext.PickupCounters.FirstOrDefault(c => c.Id == order.AssignedCell.CounterId);

            await UpdateDoorOpenCount(counter.URI);
            await UpdateCounterStatistics(counter.URI);

            return await base.Put(key, entity);
        }

        [HttpGet("counter/{counterId}/order-positions")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCounterOrderPositions([FromRoute] Guid counterId)
        {
            OrderItem[] result = await AppDbContext.OrderItems
                .Include(oi => oi.Order)
                    .ThenInclude(o => o.AssignedCell)
                        .ThenInclude(c => c.Counter)
                .Include(oi => oi.GoodOrdered)
                    .ThenInclude(g => g.Category)
                .Where(oi => oi.Order.AssignedCell.CounterId == counterId)
                .ToArrayAsync();

            if (result == null || !result.Any())
            {
                return NotFound("No orders found for the specified counterId.");
            }

            return Ok(result);
        }

        private async Task UpdateDoorOpenCount(string uri)
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var response = await client.PutAsJsonAsync(new Uri(uri + "/TechInspection/updateDoorOpenCount").ToString(), new { });
        }

        private async Task UpdateCounterStatistics(string uri)
        {
            var client = httpClientFactory.CreateClient();
            client.Timeout = TimeSpan.FromMinutes(5);
            var response = await client.PutAsJsonAsync(new Uri(uri + "/Statistics/updateStatistics").ToString(), new { });
        }
    }
}
