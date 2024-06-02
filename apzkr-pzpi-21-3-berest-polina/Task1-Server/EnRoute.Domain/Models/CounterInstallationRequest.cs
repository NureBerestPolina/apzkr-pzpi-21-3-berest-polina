using EnRoute.Domain.Constants;
using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class CounterInstallationRequest : IODataEntity
    {
        public Guid Id { get; set; }
        public string Address { get; set; } = string.Empty;
        public string PlacementDescription { get; set; } = string.Empty;
        public int CellCount { get; set; }
        public int CellWithTempControlCount { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public DateTime RequestedTime { get; set; } = DateTime.UtcNow;
        public DateTime? FulfilledTime { get; set; }
        public RequestStatus RequestStatus { get; set; } = RequestStatus.Unseen;
    }
}
