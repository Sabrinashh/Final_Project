using ColumbiaProject.Areas.Manage.ViewModels;
using ColumbiaProject.DAL;
using ColumbiaProject.Helpers;
using ColumbiaProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ColumbiaProject.Areas.Manage.Controllers
{
    [Area("manage")]
    [Authorize(Roles = "SuperAdmin,Admin,Editor")]
    public class ProductController : Controller
    {

        private readonly ColumbiaDbContext _context;
        private readonly IWebHostEnvironment _env;
        public ProductController(ColumbiaDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index(int page = 1)
        {
            var query = _context.Products
                .Include(x => x.Category)
                .Include(x => x.ProductType)
                .Include(x => x.ProductImages);


            var model = PaginatedList<Product>.Create(query, page, 7);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.ProductTypes = _context.ProductTypes.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (!_context.Categories.Any(x => x.Id == product.CategoryId))
                ModelState.AddModelError("CategoryId", "Category not found");

            if (!_context.ProductTypes.Any(x => x.Id == product.ProductTypeId))
                ModelState.AddModelError("ProductTypeId", "Product Type not found");

            _checkImageFiles(product.ImageFiles, product.PosterFile);

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.ProductTypes = _context.ProductTypes.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();

                return View();
            }

            product.ProductImages = new List<ProductImage>();

            ProductImage poster = new ProductImage
            {
                Name = FileManager.Save(product.PosterFile, _env.WebRootPath, "uploads/products"),
                PosterStatus = true,

            };

            product.ProductImages.Add(poster);



            foreach (var imgFile in product.ImageFiles)
            {
                ProductImage productImg = new ProductImage
                {
                    Name = FileManager.Save(imgFile, _env.WebRootPath, "uploads/products"),
                };
                product.ProductImages.Add(productImg);
            }

            product.ProductSizes = new List<ProductSize>();

            foreach (var sizeId in product.SizeIds)
            {
                if (!_context.Sizes.Any(x => x.Id == sizeId))
                {
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.ProductTypes = _context.ProductTypes.ToList();
                    ViewBag.Sizes = _context.Sizes.ToList();

                    ModelState.AddModelError("SizeId", "Size not found");
                    return View();
                }

                ProductSize productSize = new ProductSize
                {
                    SizeId = sizeId,
                };
                product.ProductSizes.Add(productSize);
            }


            product.CreatedAt = DateTime.UtcNow.AddHours(4);
            product.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.Products.Add(product);
            _context.SaveChanges();

            return RedirectToAction("index");
        }

        private void _checkImageFiles(List<IFormFile> images, IFormFile posterFile)
        {
            if (posterFile == null)
                ModelState.AddModelError("PosterFile", "PosterFile is required");
            else if (posterFile.ContentType != "image/png" && posterFile.ContentType != "image/jpeg")
                ModelState.AddModelError("PosterFile", "Content type must be image/png or image/jpeg!");
            else if (posterFile != null && posterFile.Length > 2097152)
                ModelState.AddModelError("PosterFile", "File size must be less than 2MB!");


            if (images != null)
            {
                foreach (var imgFile in images)
                {
                    if (imgFile.ContentType != "image/png" && imgFile.ContentType != "image/jpeg")
                        ModelState.AddModelError("ImageFiles", "Content type must be image/png or image/jpeg!");

                    if (imgFile.Length > 2097152)
                        ModelState.AddModelError("ImageFiles", "File size must be less than 2MB!");
                }
            }
        }

        public IActionResult Edit(int id)
        {
            Product product = _context.Products.Include(x => x.ProductSizes).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == id);

            if (product == null)
                return RedirectToAction("index", "error");
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.ProductTypes = _context.ProductTypes.ToList();
            ViewBag.Sizes = _context.Sizes.ToList();

            product.SizeIds = product.ProductSizes.Select(x => x.SizeId).ToList();

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            Product existProduct = _context.Products.Include(x => x.ProductSizes).Include(x => x.ProductImages).FirstOrDefault(x => x.Id == product.Id);

            if (existProduct == null)
                return RedirectToAction("index", "error");

            if (existProduct.CategoryId != product.CategoryId && !_context.Categories.Any(x => x.Id == product.CategoryId))
                ModelState.AddModelError("CategoryId", "Category not found!");

            if (existProduct.ProductTypeId != product.ProductTypeId && !_context.ProductTypes.Any(x => x.Id == product.ProductTypeId))
                ModelState.AddModelError("ProductTypeId", "Author not found!");

            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.ToList();
                ViewBag.ProductTypes = _context.ProductTypes.ToList();
                ViewBag.Sizes = _context.Sizes.ToList();

                existProduct.SizeIds = product.ProductSizes.Select(x => x.SizeId).ToList();

                return View(existProduct);
            }

            if (product.PosterFile != null)
            {
                var poster = existProduct.ProductImages.FirstOrDefault(x => x.PosterStatus == true);
                var newPosterName = FileManager.Save(product.PosterFile, _env.WebRootPath, "uploads/products");
                FileManager.Delete(_env.WebRootPath, "uploads/products", poster.Name);
                poster.Name = newPosterName;
            }


            var removedFiles = existProduct.ProductImages.FindAll(x => x.PosterStatus == null && !product.ProductImageIds.Contains(x.Id));

            foreach (var item in removedFiles)
            {
                FileManager.Delete(_env.WebRootPath, "uploads/products", item.Name);
            }

            existProduct.ProductImages.RemoveAll(x => removedFiles.Contains(x));

            foreach (var imgFile in product.ImageFiles)
            {
                ProductImage productImage = new ProductImage
                {
                    Name = FileManager.Save(imgFile, _env.WebRootPath, "uploads/products"),
                };
                existProduct.ProductImages.Add(productImage);
            }

            existProduct.ProductSizes.RemoveAll(x => !product.SizeIds.Contains(x.SizeId));

            foreach (var tagId in product.SizeIds.Where(x => !existProduct.ProductSizes.Any(bt => bt.SizeId == x)))
            {
                if (!_context.Sizes.Any(x => x.Id == tagId))
                {
                    ViewBag.Categories = _context.Categories.ToList();
                    ViewBag.ProductTypes = _context.ProductTypes.ToList();
                    ViewBag.Sizes = _context.Sizes.ToList();

                    product.SizeIds = existProduct.ProductSizes.Select(x => x.SizeId).ToList();

                    ModelState.AddModelError("TagIds", "Tag not found");
                    return View(existProduct);
                }

                ProductSize productSize = new ProductSize
                {
                    SizeId = tagId
                };
                existProduct.ProductSizes.Add(productSize);
            }


            existProduct.CategoryId = product.CategoryId;
            existProduct.ProductTypeId = product.ProductTypeId;
            existProduct.Name = product.Name;
            existProduct.SalePrice = product.SalePrice;
            existProduct.DiscountPercent = product.DiscountPercent;
            existProduct.CostPrice = product.CostPrice;
            existProduct.IsNew = product.IsNew;
            existProduct.IsSpecial = product.IsSpecial;
            existProduct.StockStatus = product.StockStatus;

            existProduct.ModifiedAt = DateTime.UtcNow.AddHours(4);

            _context.SaveChanges();

            return RedirectToAction("index");
        }

        public IActionResult Delete(int id)
        {
            Product product = _context.Products.Include(x => x.Category).Include(x => x.ProductType).Include(x => x.ProductSizes).ThenInclude(x => x.Size).FirstOrDefault(x => x.Id == id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            Product existProduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);

           
                _context.Products.Remove(existProduct);
                _context.SaveChanges();
                product.IsDeleted = true;

                return RedirectToAction("index");
        }
    }
}

