namespace PickupCounterIoT.Models
{
    public class Good
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MeasurementUnit { get; set; }
        public bool NeedsCooling { get; set; } = false;
        public decimal Price { get; set; }
        public double AmountInStock { get; set; }
        public Guid ProducerId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid OrganizationId { get; set; }
    }
}
