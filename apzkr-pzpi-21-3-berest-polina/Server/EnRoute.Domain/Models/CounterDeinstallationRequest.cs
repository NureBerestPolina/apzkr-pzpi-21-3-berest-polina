using EnRoute.Domain.Constants;
using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class CounterDeinstallationRequest : IODataEntity
    {
        public Guid Id { get; set; }
        public DateTime RequestedTime { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledTime { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Unseen;
        public Guid CounterId { get; set; }
        public PickupCounter Counter { get; set; }
    }
}
