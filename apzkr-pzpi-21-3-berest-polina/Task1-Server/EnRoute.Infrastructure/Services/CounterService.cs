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

namespace EnRoute.Infrastructure.Services
{
    public class CounterService : ICounterService
    {
        private readonly ApplicationDbContext dbContext;

        public CounterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool CheckIfUriExists(string uri)
        {
            return dbContext.PickupCounters.Any(f => f.URI == uri);
        }

        public async Task InstallCounterAsync(CounterInstallationRequest request, string uri)
        {
            var counterToInstall = new PickupCounter
            {
                Address = request.Address,
                PlacementDescription = request.PlacementDescription,
                CellCount = request.CellCount,
                CellWithTempControlCount = request.CellWithTempControlCount,
                OrganizationId = request.OrganizationId,
                URI = uri
            };

            
            AddCounterCells(counterToInstall);

            dbContext.PickupCounters.Add(counterToInstall);
            await dbContext.SaveChangesAsync();
        }

        private void AddCounterCells(PickupCounter counterToInstall)
        {
            for (int i = 0; i < counterToInstall.CellCount; i++)
            {
                var cell = new Cell
                {
                    hasTemperatureControl = i < counterToInstall.CellWithTempControlCount,
                    CounterId = counterToInstall.Id,
                };

                counterToInstall.Cells.Add(cell);
            }
        }
    }
}
