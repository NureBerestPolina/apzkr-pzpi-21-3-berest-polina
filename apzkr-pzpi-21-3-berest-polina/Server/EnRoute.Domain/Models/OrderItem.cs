using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class OrderItem : IODataEntity
    {
        public Guid Id { get; set; }
        public int Count { get; set; }
        public Guid GoodId { get; set; }
        public Good GoodOrdered { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
    }
}
