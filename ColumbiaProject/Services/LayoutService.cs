using ColumbiaProject.DAL;
using ColumbiaProject.Models;
using ColumbiaProject.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;

namespace ColumbiaProject.Services
{
    public class LayoutService
    {
        private readonly ColumbiaDbContext _context;
        private readonly IHttpContextAccessor _httpAccessor;

        public LayoutService(ColumbiaDbContext context, IHttpContextAccessor httpAccessor)
        {
            _context = context;
            _httpAccessor = httpAccessor;
    
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }
        public BasketViewModel GetBasket()
        {
            BasketViewModel basket = new BasketViewModel();

            if (_httpAccessor.HttpContext.User.Identity.IsAuthenticated && _httpAccessor.HttpContext.User.IsInRole("Member"))
            {
                string userId = _httpAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);


                var model = _context.BasketItems.Include(x => x.Product).ThenInclude(x => x.ProductImages).Where(x => x.AppUserId == userId).ToList();


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
                var basketStr = _httpAccessor.HttpContext.Request.Cookies["basket"];

                List<BasketItemCookieViewModel> basketCookieItems = new List<BasketItemCookieViewModel>();

                if (basketStr != null)
                {
                    basketCookieItems = JsonConvert.DeserializeObject<List<BasketItemCookieViewModel>>(basketStr);
                }
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



            return basket;
        }

    }
}
