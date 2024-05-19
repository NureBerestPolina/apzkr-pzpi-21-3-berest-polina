using EnRoute.Domain.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models
{
    public class Category : IODataEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
