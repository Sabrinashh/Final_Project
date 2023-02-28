using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Areas.Manage.Controllers
{
    public class ErrorController : Controller
    {
        [Area("manage")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
