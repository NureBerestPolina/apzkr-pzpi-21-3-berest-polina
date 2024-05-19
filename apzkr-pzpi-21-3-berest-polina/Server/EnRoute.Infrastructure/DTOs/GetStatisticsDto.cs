using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.DTOs
{
    public class GetStatisticsDto
    {
        public string OrganizationName { get; set; }
        public string ManagerName { get; set; }
        public string Contact { get; set; }
        public int PickupCountersCount { get; set; }
        public int OrdersCount { get; set; }
        public decimal TotalSalesSum { get; set; }
    }
}
