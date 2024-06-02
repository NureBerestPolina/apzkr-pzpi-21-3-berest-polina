using EnRoute.Domain.Models;
using EnRoute.Domain;

namespace EnRoute.API.Controllers
{
    public class ProducersController : ODataControllerBase<Producer>
    {
        public ProducersController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
