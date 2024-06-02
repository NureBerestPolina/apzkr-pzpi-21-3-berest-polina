using EnRoute.Domain.Models;

namespace EnRoute.API.Contracts.Auth.Responses
{
    public class GetOrganizationStatisticsResponse
    {
        public Guid PickupCounterId { get; set; }
        public string PickupCounterAddress { get; set; }
        public string PickupCounterPlacementDescription { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalSalesSum { get; set; }
        public Good MostPopularGood { get; set; }
    }
}
