using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class Good : IODataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MeasurementUnit { get; set; }
        public bool NeedsCooling { get; set; } = false;
        public decimal Price { get; set; }
        public double AmountInStock { get; set; }
        public Guid ProducerId { get; set; }
        public Producer Producer { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid OrganizationId { get; set; }
        public Organization Organization { get; set; }
    }
}
