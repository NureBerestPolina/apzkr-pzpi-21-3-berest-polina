using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class PickupCounter : IODataEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Address { get; set; } = string.Empty;
        public string PlacementDescription { get; set; } = string.Empty;
        public string? URI { get; set; } = string.Empty;
        public int CellCount { get; set; }
        public int CellWithTempControlCount { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Guid OrganizationId { get; set; }
        public Organization? OwnerOrganization { get; set; }
        public List<Cell>? Cells { get; set; } = new List<Cell>();
    }
}
