using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ColumbiaProject.Areas.Manage.Controllers
{

    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class SettingController : Controller
    {

        private readonly ColumbiaDbContext _context;

        public SettingController(ColumbiaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            var model = _context.Settings.Skip((page - 1) * 4).Take(4).ToList();
            ViewBag.Page = page;
            ViewBag.TotalPage = (int)Math.Ceiling(_context.Settings.Count() / 5d);
            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (_context.Settings.Any(x => x.Key == setting.Key))
            {
                ModelState.AddModelError("Key", "This key has been taken");
                return View();
            }
            if (_context.Settings.Any(x => x.Value == setting.Value))
            {
                ModelState.AddModelError("Value", "This value has been taken");
                return View();
            }

            _context.Settings.Add(setting);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Setting setting = _context.Settings.FirstOrDefault(x => x.Id == id);

            if (setting == null)
                return RedirectToAction("index", "error");
            return View(setting);
        }


        [HttpPost]
        public IActionResult Edit(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            if (_context.Settings.Any(x => x.Key == setting.Key && x.Id != setting.Id))
            {
                ModelState.AddModelError("Key", "This key has been taken");
                return View();
            }
            if (_context.Settings.Any(x => x.Value == setting.Value && x.Id != setting.Id))
            {
                ModelState.AddModelError("Value", "This value has been taken");
                return View();
            }

            Setting existSetting = _context.Settings.FirstOrDefault(x => x.Id == setting.Id);

            existSetting.Key = setting.Key;
            existSetting.Value = setting.Value;
            _context.SaveChanges();

            return RedirectToAction("index");
        }

    }
}
