using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.EntityFrameworkCore;
using EnRoute.Domain.Constants;
using EnRoute.Infrastructure.Services.Interfaces;

namespace EnRoute.API.Controllers
{
    public class CounterInstallationRequestsController : ODataControllerBase<CounterInstallationRequest>
    {
        private readonly ICounterService counterService;

        public CounterInstallationRequestsController(ApplicationDbContext appDbContext, ICounterService counterService) : base(appDbContext)
        {
            this.counterService = counterService;
        }

        public async override Task<IActionResult> Put([FromODataUri] Guid key, [FromBody] CounterInstallationRequest entity)
        {
            var request = await AppDbContext.CounterInstallationRequests.FirstOrDefaultAsync(f => f.Id == key);

            if (request == null)
                return NotFound();

            request.RequestStatus = entity.RequestStatus;
            request.FulfilledTime = DateTime.UtcNow;

            await AppDbContext.SaveChangesAsync();

            return Ok(request);
        }

        [HttpPost]
        [Route("counter-install-requests/{installRequestId}/fulfill")]
        public async Task<IActionResult> FulfillRequest(Guid installRequestId, string uri)
        {
            var installRequest = await AppDbContext.CounterInstallationRequests.FirstOrDefaultAsync(f => f.Id == installRequestId);

            if (installRequest == null)
            {
                return NotFound();
            }

            installRequest.RequestStatus = RequestStatus.Fulfilled;
            installRequest.FulfilledTime = DateTime.UtcNow;

            await counterService.InstallCounterAsync(installRequest, uri);
            return Ok(installRequest);
        }

        [HttpGet("counter-install-requests/checkUri")]
        public ActionResult<bool> CheckUrl(string uri)
        {
            bool uriExists = counterService.CheckIfUriExists(uri);
            return Ok(uriExists);
        }
    }
}
