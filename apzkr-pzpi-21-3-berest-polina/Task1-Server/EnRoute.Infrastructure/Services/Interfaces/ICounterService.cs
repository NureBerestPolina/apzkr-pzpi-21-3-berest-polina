using EnRoute.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnRoute.Infrastructure.Services.Interfaces
{
    public interface ICounterService
    {
        bool CheckIfUriExists(string uri);
        Task InstallCounterAsync(CounterInstallationRequest request, string uri);
    }
}
