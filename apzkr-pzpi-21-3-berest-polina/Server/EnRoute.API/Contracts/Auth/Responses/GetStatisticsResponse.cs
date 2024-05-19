namespace EnRoute.API.Contracts.Auth.Responses
{
    public class GetStatisticsResponse
    {
        public string OrganizationName { get; set; }
        public string ManagerName { get; set; }
        public string Contact { get; set; }
        public int PickupCountersCount { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalSalesSum { get; set; }
    }
}
