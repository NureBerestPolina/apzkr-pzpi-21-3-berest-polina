using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using EnRoute.Domain.Constants;
using EnRoute.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Azure.Core;
using EnRoute.Infrastructure.Commands;
using EnRoute.API.Contracts.Auth.Responses;

namespace EnRoute.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class StatisticsController : ControllerBase
    { 
        private readonly IStatisticsService statisticsService;
        private readonly IMapper mapper;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper) 
        {
            this.statisticsService = statisticsService;
            this.mapper = mapper;
        }

        [HttpGet("admin-statistics")]
        public async Task<IActionResult> GetAdminStatistics()
        {
            var statistics = await statisticsService.GetAllOrganizationsStatisticsAsync();
            var statisticsResponse = mapper.Map<List<GetStatisticsResponse>>(statistics);

            return Ok(statisticsResponse);
        }

        [HttpGet("organization-statistics/{id}")]
        public async Task<IActionResult> GetOrganizationStatistics(Guid id)
        {
            var statistics = await statisticsService.GetOrganizationStatisticsAsync(id);
            var statisticsResponse = mapper.Map<List<GetOrganizationStatisticsResponse>>(statistics);

            return Ok(statisticsResponse);
        }
    }
}
