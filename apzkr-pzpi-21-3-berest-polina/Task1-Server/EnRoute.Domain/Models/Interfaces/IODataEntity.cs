using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Domain.Models.Interfaces
{
    public interface IODataEntity
    {
        Guid Id { get; set; }
    }
}
