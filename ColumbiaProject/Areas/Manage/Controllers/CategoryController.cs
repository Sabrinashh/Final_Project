using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace ColumbiaProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class CategoryController : Controller
    {

        private readonly ColumbiaDbContext _context;

        public CategoryController (ColumbiaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page=1)
        {
            var model = _context.Categories.Include(x => x.Products).Skip((page - 1) * 1).Take(1).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Categories.Count() / 1d);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Categories.Any(x => x.Name == category.Name))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Categories.FirstOrDefault(x => x.Id == id);

            if (category == null)
                return RedirectToAction("index", "error");
            return View(category);
        }


        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Categories.Any(x => x.Name == category.Name && x.Id != category.Id))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            Category existcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            existcategory.Name = category.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Include(x => x.Products).FirstOrDefault(x => x.Id == id);

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            Category existcategory = _context.Categories.FirstOrDefault(x => x.Id == category.Id);

            if (!_context.Products.Any(x => x.CategoryId == category.Id))
            {
                _context.Categories.Remove(existcategory);
                _context.SaveChanges();
            }


            return RedirectToAction("index");
        }
    }
}

