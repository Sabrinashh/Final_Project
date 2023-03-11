using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using ColumbiaProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;

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

        public async Task<IActionResult> DetailAsync(int id)
        {
            Product product=_context.Products.Include(x => x.Category)
                .Include(x => x.ProductType).Include(x => x.ProductImages).Include(x=>x.Reviews).ThenInclude(x=>x.AppUser)
                .Include(x => x.ProductSizes).ThenInclude(x => x.Size).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                TempData["error"] = "Mehsul yoxdur";
                return RedirectToAction("index", "home");
            }

            ProductDetailViewModel detailVM = new ProductDetailViewModel
            {
                Product = product,
                ReviewVM = new ReviewCreateViewModel {ProductId = id },
            };

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (user != null)
                {
                    detailVM.HasReview = product.Reviews.Any(x => x.AppUserId == user.Id);
                }
            }
            return View(detailVM);   
        }


        [Authorize(Roles = "Member")]
        [HttpPost]
        public async Task<IActionResult> Review(ReviewCreateViewModel reviewVM)
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            Product product = await _context.Products
              .Include(x => x.Category)
              .Include(x => x.ProductType)
              .Include(x => x.ProductImages)
              .Include(x => x.Reviews).ThenInclude(x => x.AppUser)
              .Include(x => x.ProductSizes).ThenInclude(x => x.Size)
              .FirstOrDefaultAsync(x => x.Id == reviewVM.ProductId);

            if (product == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ProductDetailViewModel detailVM = new ProductDetailViewModel
                {
                    Product = product,
                    ReviewVM = reviewVM,
                    HasReview = product.Reviews.Any(x => x.AppUserId == user.Id)
                };

                return View("detail", detailVM);
            }

            Review newReview = new Review { 
                Text = reviewVM.Text,
                AppUserId = user.Id,
                CreatedAt = DateTime.UtcNow.AddHours(4)
            };

            product.Reviews.Add(newReview);
            await _context.SaveChangesAsync();

            return RedirectToAction("detail", new { id = product.Id });
        }

        public async Task<IActionResult> AddToBasket(int productId, int count = 1)
        {
            AppUser user = null;


            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }


            if (!_context.Products.Any(x => x.Id == productId && x.StockStatus))
            {
                return NotFound();
            }

            BasketViewModel basket = new BasketViewModel();


            if (user != null)
            {
                BasketItem basketItem = _context.BasketItems.FirstOrDefault(x => x.ProductId == productId && x.AppUserId == user.Id);

                if (basketItem == null)
                {
                    basketItem = new BasketItem
                    {
                        AppUserId = user.Id,
                        ProductId = productId,
                        Count = 1,
                        CreatedDate = DateTime.UtcNow.AddHours(4)
                    };

                    _context.BasketItems.Add(basketItem);
                }
                else
                {
                    basketItem.Count++;
                }

                _context.SaveChanges();

                var model = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == user.Id).ToList();


                foreach (var item in model)
                {
                    BasketItemViewModel itemVM = new BasketItemViewModel
                    {
                        Product = item.Product,
                        Count = item.Count,
                        Id = item.Id
                    };

                    basket.Items.Add(itemVM);
                    basket.TotalPrice += item.Count * (item.Product.SalePrice * (100 - item.Product.DiscountPercent) / 100);
                }
            }
            else
            {
                var basketStr = HttpContext.Request.Cookies["basket"];

                List<BasketItemCookieViewModel> basketCookieItems = null;
                if (basketStr == null)
                {
                    basketCookieItems = new List<BasketItemCookieViewModel>();
                }
                else
                {
                    basketCookieItems = JsonConvert.DeserializeObject<List<BasketItemCookieViewModel>>(basketStr);
                }


                BasketItemCookieViewModel basketCookieItem = basketCookieItems.FirstOrDefault(x => x.ProductId == productId);

                if (basketCookieItem == null)
                {
                    basketCookieItem = new BasketItemCookieViewModel
                    {
                        ProductId = productId,
                        Count = 1
                    };

                    basketCookieItems.Add(basketCookieItem);
                }
                else
                {
                    basketCookieItem.Count++;
                }


                var jsonStr = JsonConvert.SerializeObject(basketCookieItems);
                HttpContext.Response.Cookies.Append("basket", jsonStr);



                foreach (var item in basketCookieItems)
                {
                    Product Product = _context.Products.Include(x => x.ProductImages).FirstOrDefault(x => x.Id == item.ProductId);

                    BasketItemViewModel itemVM = new BasketItemViewModel
                    {
                        Product = Product,
                        Count = item.Count,
                        Id = 0
                    };

                    basket.Items.Add(itemVM);
                    basket.TotalPrice += item.Count * (itemVM.Product.SalePrice * (100 - itemVM.Product.DiscountPercent) / 100);
                }
            }



            return RedirectToAction("index", "card");
        }


        public IActionResult GetBasket()
        {
            var basketStr = HttpContext.Request.Cookies["basket"];

            var basket = JsonConvert.DeserializeObject<List<BasketItemCookieViewModel>>(basketStr);

            return Ok(basket);
        }
    }
}
