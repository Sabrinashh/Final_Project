using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ColumbiaProject.Controllers
{
    public class ProductController : Controller
    {
        private readonly ColumbiaDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ProductController(ColumbiaDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Detail(int id)
        {
            Product product=_context.Products.Include(x => x.Category)
                .Include(x => x.ProductType).Include(x => x.ProductImages)
                .Include(x => x.ProductSizes).ThenInclude(x => x.Size).FirstOrDefault(p => p.Id == id);

            return View(product);
        }
    }
}
