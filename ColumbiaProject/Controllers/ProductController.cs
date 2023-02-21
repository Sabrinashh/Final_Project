using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail()
        {
            return View();
        }
    }
}
