using EnRoute.Domain.Models;
using EnRoute.Domain;

namespace EnRoute.API.Controllers
{
    public class CategoriesController : ODataControllerBase<Category>
    {
        public CategoriesController(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
