using EnRoute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.DTOs
{
    public class GetOrganizationStatisticsDto
    {
        public Guid PickupCounterId { get; set; }
        public string PickupCounterAddress { get; set; }
        public string PickupCounterPlacementDescription { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalSalesSum { get; set; }
        public Good MostPopularGood { get; set; }
    }
}
