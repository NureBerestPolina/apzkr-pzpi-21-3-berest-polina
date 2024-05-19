using EnRoute.Common.Configuration;
using EnRoute.Domain.Models;
using EnRoute.Domain;
using EnRoute.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnRoute.Infrastructure.DTOs;
using Microsoft.EntityFrameworkCore;

namespace EnRoute.Infrastructure.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly ApplicationDbContext dbContext;

        public StatisticsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetStatisticsDto>> GetAllOrganizationsStatisticsAsync()
        {
            var statistics = await dbContext.Organizations
                                            .Include(org => org.Manager)
                                            .Include(org => org.Counters)
                                            .ThenInclude(counter => counter.Cells)
                                            .Include(org => org.Goods)
                                            .Include(org => org.Manager)
                                            .Select(org => new GetStatisticsDto
                                            {
                                                OrganizationName = org.Name,
                                                ManagerName = org.Manager.Name,
                                                Contact = org.Manager.Email,
                                                PickupCountersCount = org.Counters.Count,
                                                OrdersCount = org.Counters
                                                    .SelectMany(counter => counter.Cells.Select(cell => cell.Order))
                                                    .Where(order => order != null)
                                                    .Count(),
                                                TotalSalesSum = org.Counters
                                                    .SelectMany(counter => counter.Cells.Select(cell => cell.Order))
                                                    .Where(order => order != null)
                                                    .SelectMany(order => order.Items)
                                                    .Sum(item => item.GoodOrdered.Price * item.Count)
                                            })
                                            .ToListAsync();

            return statistics;
        }

        public async Task<List<GetOrganizationStatisticsDto>> GetOrganizationStatisticsAsync(Guid organizationId)
        {
            var organizationStatistics = await dbContext.PickupCounters
                                                        .Where(counter => counter.OrganizationId == organizationId)
                                                        .Select(counter => new GetOrganizationStatisticsDto
                                                        {
                                                            PickupCounterId = counter.Id,
                                                            PickupCounterAddress = counter.Address,
                                                            PickupCounterPlacementDescription = counter.PlacementDescription,
                                                            OrdersCount = counter.Cells
                                                                .Select(cell => cell.Order)
                                                                .Where(order => order != null )
                                                                .Count(),
                                                            TotalSalesSum = counter.Cells
                                                                .Select(cell => cell.Order)
                                                                .Where(order => order != null )
                                                                .SelectMany(order => order.Items)
                                                                .Sum(item => item.GoodOrdered.Price * item.Count),
                                                            MostPopularGood = counter.Cells
                                                                .Select(cell => cell.Order)
                                                                .Where(order => order != null )
                                                                .SelectMany(order => order.Items)
                                                                .GroupBy(item => item.GoodOrdered)
                                                                .OrderByDescending(group => group.Sum(item => item.Count))
                                                                .Select(group => group.Key)
                                                                .FirstOrDefault()
                                                        })
                                                        .ToListAsync();

                                                            return organizationStatistics;
                                                        }
    }
}
