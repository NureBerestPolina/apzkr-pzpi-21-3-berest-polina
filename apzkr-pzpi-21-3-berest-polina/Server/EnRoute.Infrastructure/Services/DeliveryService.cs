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
using EnRoute.Domain.Constants;

namespace EnRoute.Infrastructure.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ApplicationDbContext dbContext;

        public DeliveryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<GetCounterDeliveryDto>> GetCounterDeliveryList(Guid counterId)
        {
            var counterDeliveries = await dbContext.Cells.Include(c => c.Order)
                    .Where(c => c.CounterId == counterId)
                    .Select(c => new GetCounterDeliveryDto
                    {
                        CellId = c.Id,
                        OrderId = c.Order.Id,
                        CellUnlockCode = c.CellOpenKey
                    })
                    .ToListAsync();

            return counterDeliveries;
        }

        public async Task<List<GetDeliveryDestinationDto>> GetDeliveryList(Guid organizationId)
        {
            var deliveryDestinations = await dbContext.PickupCounters
                                        .Where(c => c.OrganizationId == organizationId)
                                        .SelectMany(c => c.Cells)
                                        .Where(cell => cell.Order.Status == OrderStatus.New)
                                        .Select(cell => new GetDeliveryDestinationDto
                                        {
                                            CounterId = cell.CounterId,
                                            CellId = cell.Id,
                                            OrderId = cell.Order.Id,
                                            CellUnlockCode = cell.CellOpenKey
                                        })
                                        .ToListAsync();

            return deliveryDestinations;
        }
    }
}