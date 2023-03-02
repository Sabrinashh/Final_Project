using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using ColumbiaProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ColumbiaProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ColumbiaDbContext _context;

        public HomeController(ColumbiaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {

            HomeViewModel homeVM = new HomeViewModel
            {
                Headers = _context.Headers.ToList(),
                Settings = _context.Settings.ToDictionary(x => x.Key, x => x.Value),
                Products = (List<Product>)_context.Products.Include(x => x.Category)
                .Include(x => x.ProductType).Include(x => x.ProductImages)
                .Include(x => x.ProductSizes).ThenInclude(x => x.Size).Take(4).ToList(),
            };
            return View(homeVM);
        }


    }
}