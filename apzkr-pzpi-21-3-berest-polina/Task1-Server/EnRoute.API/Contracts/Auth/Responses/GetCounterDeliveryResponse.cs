namespace EnRoute.API.Contracts.Auth.Responses
{
    public class GetCounterDeliveryResponse
    {
        public Guid CellId { get; set; }
        public Guid OrderId { get; set; }
        public string CellUnlockCode { get; set; }
    }
}
