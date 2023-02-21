using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Controllers
{
    public class CardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
