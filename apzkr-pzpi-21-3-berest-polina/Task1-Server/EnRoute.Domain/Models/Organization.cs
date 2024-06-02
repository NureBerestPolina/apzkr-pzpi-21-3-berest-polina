using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class Organization : IODataEntity
    {
        public Guid Id { get; set; }
        public bool IsBlocked { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.UtcNow;
        public Guid ManagerId { get; set; }
        public User Manager { get; set; }
        public List<PickupCounter>? Counters { get; set; } = new List<PickupCounter>();
        public List<Good>? Goods { get; set; } = new List<Good>();
    }
}
