using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
