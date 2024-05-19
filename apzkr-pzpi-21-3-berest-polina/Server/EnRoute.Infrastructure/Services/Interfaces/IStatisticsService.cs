using EnRoute.Domain.Models;
using EnRoute.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<List<GetStatisticsDto>> GetAllOrganizationsStatisticsAsync();
        Task<List<GetOrganizationStatisticsDto>> GetOrganizationStatisticsAsync(Guid organizationId);
    }
}
