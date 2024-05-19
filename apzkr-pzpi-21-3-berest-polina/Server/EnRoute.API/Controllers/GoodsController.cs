using EnRoute.Domain.Models;
using EnRoute.Domain;

namespace EnRoute.API.Controllers
{
    public class GoodsController : ODataControllerBase<Good>
    {
        public GoodsController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
