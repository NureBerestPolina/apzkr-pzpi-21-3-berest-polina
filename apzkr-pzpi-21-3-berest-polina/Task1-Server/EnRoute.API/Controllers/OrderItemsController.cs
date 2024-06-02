using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnRoute.API.Controllers
{
    public class OrderItemsController : ODataControllerBase<OrderItem>
    {
        public OrderItemsController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }

        [HttpGet("order-recommendations/{userId}")]
        public IActionResult GetOrderRecommendations(Guid userId)
        {
            var allUserOrderItems = AppDbContext.OrderItems.AsNoTracking()
                                                    .Include(oi => oi.Order)
                                                    .Where(oi => oi.Order.CustomerId == userId)
                                                    .Include(o => o.GoodOrdered)
                                                    .Include(o => o.Order.AssignedCell.Counter)
                                                    .ToList();
            if (allUserOrderItems == null)
            {
                return NotFound();
            }

            var availableGoods = allUserOrderItems.Where(oi => oi.GoodOrdered.AmountInStock > 0)
                                        .Select(oi => oi.GoodOrdered)
                                        .Distinct()
                                        .ToList();

            if (availableGoods.Any())
            {
                var uniqueGoods = availableGoods.GroupBy(g => g.Id).Select(group => group.First()).ToList();
                return Ok(uniqueGoods);
            }

            return Ok(new List<Good>());
        }

    }
}
