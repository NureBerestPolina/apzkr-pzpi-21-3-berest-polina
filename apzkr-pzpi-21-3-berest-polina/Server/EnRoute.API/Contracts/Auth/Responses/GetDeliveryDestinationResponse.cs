namespace EnRoute.API.Contracts.Auth.Responses
{
    public class GetDeliveryDestinationResponse
    {
        public Guid CounterId { get; set; }
        public Guid CellId { get; set; }
        public Guid OrderId { get; set; }
        public string CellUnlockCode { get; set; }
    }
}
