namespace PickupCounterIoT.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Guid GoodId { get; set; }
        public Good GoodOrdered { get; set; }
        public Guid OrderId { get; set; }
    }
}
