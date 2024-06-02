using EnRoute.Domain.Constants;
using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class TechInspectionRequest : IODataEntity
    {
        public Guid Id { get; set; }
        public DateTime RequestedTime { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledTime { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.Unseen;
        public Guid CellId { get; set; }
        public Cell Cell { get; set; }
        public decimal Temperature { get; set; }
        public int OpensCount { get; set; }
    }
}
