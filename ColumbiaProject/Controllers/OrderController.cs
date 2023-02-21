using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        }
    }
}
