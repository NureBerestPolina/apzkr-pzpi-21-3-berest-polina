using EnRoute.Domain.Models.Interfaces;
using EnRoute.Domain.Constants;

namespace EnRoute.Domain.Models
{
    public class Order : IODataEntity
    {
        public Guid Id { get; set; }
        public DateTime OrderedDate { get; set; } = DateTime.UtcNow;
        public DateTime? FinalizedDate { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.New;
        public Guid AssignedCellId { get; set; }
        public Cell? AssignedCell { get; set; }
        public Guid CustomerId { get; set; }
        public User? Customer { get; set; }
        public List<OrderItem>? Items { get; set; } = new List<OrderItem>();
    }
}
