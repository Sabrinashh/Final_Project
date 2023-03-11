using ColumbiaProject.Areas.Manage.ViewModels;
using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;

namespace ColumbiaProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class DashBoardController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ColumbiaDbContext _context;


        public DashBoardController(ColumbiaDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;

        }
        public IActionResult Index()
        {
            DashboardViewModel dashboardVM = new DashboardViewModel
            {
                Products = _context.Products.Include(x => x.ProductSizes).ThenInclude(x => x.Size).Include(x => x.Category).Include(x => x.ProductType).ToList(),
                Categories=_context.Categories.Include(x=>x.Products).ToList(),
                Users=_userManager.Users.ToList(),
                Orders=_context.Orders.ToList(),
                ProductTypes=_context.ProductTypes.ToList()
            };
            return View(dashboardVM);
        }
    }
}

 