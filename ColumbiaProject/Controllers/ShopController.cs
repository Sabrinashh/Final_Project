using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using ColumbiaProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Reflection.Metadata.BlobBuilder;

namespace ColumbiaProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly ColumbiaDbContext _context;

        public ShopController(ColumbiaDbContext context)
        {
            _context = context;
        }
       
            public IActionResult Index(int page = 1, List<int>? categoryIds = null, List<int>? productTypeIds = null, List<int> sizeIds = null, decimal? minPrice = null, decimal? maxPrice = null, string sort = "AtoZ")
            {
                ViewBag.SelectedCategoryId = categoryIds;
                ViewBag.SelectedProductTypeId = productTypeIds;
            ViewBag.SelectedSizeId = sizeIds;   


                var products = _context.Products.Include(x => x.Category).Include(x => x.ProductImages).AsQueryable();
            var productSizes = _context.ProductSizes.Include(x => x.Size).AsQueryable();
            if (categoryIds != null && categoryIds.Count > 0)
                products = products.Where(x => categoryIds.Contains(x.CategoryId));
            if (productTypeIds != null && productTypeIds.Count > 0)
                products = products.Where(x => productTypeIds.Contains(x.ProductTypeId));
            if (sizeIds != null && sizeIds.Count > 0)
                productSizes = productSizes.Where(x => sizeIds.Contains(x.SizeId));
                    



            if (minPrice != null && maxPrice != null)
                {
                    products = products.Where(x => x.SalePrice >= minPrice && x.SalePrice <= maxPrice);

                }

                switch (sort)
                {
                    case "ZToA":
                        products = products.OrderByDescending(x => x.Name);
                        break;
                    case "LowToHigh":
                        products = products.OrderBy(x => x.SalePrice);
                        break;
                    case "HighToLow":
                        products = products.OrderByDescending(x => x.SalePrice);
                        break;
                    default:
                        products = products.OrderBy(x => x.Name);
                        break;
                }




                ShopViewModel model = new ShopViewModel
                {
                    Products = PaginatedList<Product>.Create(products, page, 12),
                    Categories = _context.Categories.Include(x => x.Products).Where(x => x.Products.Count > 0).ToList(),
                    ProductTypes = _context.ProductTypes.Include(x => x.Products).Where(x => x.Products.Count > 0).ToList(),
                    Sizes = _context.Sizes.ToList(),
                    MinPrice = _context.Products.Min(x => x.SalePrice),
                    MaxPrice = _context.Products.Max(x => x.SalePrice),
                };

                ViewBag.SelectedMinPrice = minPrice ?? model.MinPrice;
                ViewBag.SelectedMaxPrice = maxPrice ?? model.MaxPrice;

                return View(model);
            }
    
        }
    }

