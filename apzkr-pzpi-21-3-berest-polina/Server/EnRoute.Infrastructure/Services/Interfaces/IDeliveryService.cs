using EnRoute.Domain.Models;
using EnRoute.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<List<GetDeliveryDestinationDto>> GetDeliveryList(Guid organizationCode);
        Task<List<GetCounterDeliveryDto>> GetCounterDeliveryList(Guid counterId);
    }
}
