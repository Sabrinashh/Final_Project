using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Areas.Manage.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
