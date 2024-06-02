using EnRoute.Domain.Models;
using EnRoute.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnRoute.API.Controllers
{
    public class TechInspectionRequestsController : ODataControllerBase<TechInspectionRequest>
    {
        private readonly IHttpClientFactory httpClientFactory;

        public TechInspectionRequestsController(ApplicationDbContext appDbContext,
            IHttpClientFactory httpClientFactory) : base(appDbContext)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async override Task<IActionResult> Delete([FromRoute] Guid key)
        {
            var techInspectionRequest = await AppDbContext.TechInspectionRequests.FindAsync(key);
            if (techInspectionRequest == null)
            {
                return NotFound();
            }

            var cell = await AppDbContext.Cells.Include(c => c.Counter).FirstOrDefaultAsync(f => f.Id == techInspectionRequest.CellId);

            if (cell != null)
                await ResetValues(cell.Counter.URI);

            return await base.Delete(key);
        }

        [AllowAnonymous]
        public override Task<IActionResult> Post([FromBody] TechInspectionRequest entity)
        {
            return base.Post(entity);
        }

        private async Task ResetValues(string uri)
        {
            var client = httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, new Uri(uri + "/TechInspection"));
            var response = await client.PostAsJsonAsync(new Uri(uri + "/TechInspection").ToString(), new { });
        }
    }
}

