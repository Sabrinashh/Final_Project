using Microsoft.AspNetCore.Mvc;

namespace ColumbiaProject.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult ResetPassword()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
