using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class Cell : IODataEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool IsFree { get; set; } = true;
        public bool hasTemperatureControl { get; set; } = false;
        public Guid CounterId { get; set; }
        public PickupCounter Counter { get; set; }
        public string CellOpenKey { get; set; } = DateTime.UtcNow.ToString();
        public Order Order { get; set; } 
    }
}
