using ColumbiaProject.Models;

namespace ColumbiaProject.ViewModels
{
    public class ShopViewModel
    {
        public PaginatedList<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Size> Sizes { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
    }
}
