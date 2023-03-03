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
    public class SizeController : Controller
    {

        private readonly ColumbiaDbContext _context;
        public SizeController(ColumbiaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Sizes.Skip((page - 1) * 4).Take(4).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Categories.Count() / 1d);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Sizes.Any(x => x.Name == size.Name))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            _context.Sizes.Add(size);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Size size = _context.Sizes.FirstOrDefault(x => x.Id == id);

            if (size == null)
                return RedirectToAction("index", "error");
            return View(size);
        }


        [HttpPost]
        public IActionResult Edit(Size size)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Sizes.Any(x => x.Name == size.Name && x.Id != size.Id))
            {
                ModelState.AddModelError("Name", "This name has been taken");
                return View();
            }

            Size existSize = _context.Sizes.FirstOrDefault(x => x.Id == size.Id);

            existSize.Name = size.Name;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Size size = _context.Sizes.FirstOrDefault(x => x.Id == id);

            return View(size);
        }

        [HttpPost]
        public IActionResult Delete(Size size)
        {
            Size existSize = _context.Sizes.FirstOrDefault(x => x.Id == size.Id);

           
                _context.Sizes.Remove(existSize);
                _context.SaveChanges();
            


            return RedirectToAction("index");
        }
    }
}
