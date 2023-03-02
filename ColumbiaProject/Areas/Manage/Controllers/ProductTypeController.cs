using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;


namespace ColumbiaProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class ProductTypeController : Controller
    {
        private readonly ColumbiaDbContext _context;

        public ProductTypeController(ColumbiaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.ProductTypes.Include(x => x.Products).Skip((page - 1) * 3).Take(3).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.ProductTypes.Count() / 3d);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductType ProductType)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.ProductTypes.Any(x => x.Name == ProductType.Name))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            _context.ProductTypes.Add(ProductType);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            ProductType ProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == id);

            if (ProductType == null)
                return RedirectToAction("index", "error");

            return View(ProductType);
        }


        [HttpPost]
        public IActionResult Edit(ProductType ProductType)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.ProductTypes.Any(x => x.Name == ProductType.Name && x.Id != ProductType.Id))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            ProductType existProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == ProductType.Id);

            existProductType.Name = ProductType.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            ProductType ProductType = _context.ProductTypes.Include(x => x.Products).FirstOrDefault(x => x.Id == id);

            return View(ProductType);
        }

        [HttpPost]
        public IActionResult Delete(ProductType ProductType)
        {
           ProductType existProductType = _context.ProductTypes.FirstOrDefault(x => x.Id == ProductType.Id);

            if (!_context.Products.Any(x => x.ProductTypeId == ProductType.Id))
            {
                _context.ProductTypes.Remove(existProductType);
                _context.SaveChanges();
            }


            return RedirectToAction("index");
        }
    }
}
