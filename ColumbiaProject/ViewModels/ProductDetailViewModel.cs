using ColumbiaProject.Models;

namespace ColumbiaProject.ViewModels
{
    public class ProductDetailViewModel
    {
        public Product Product { get; set; }
        public ReviewCreateViewModel ReviewVM { get; set; }
        public bool HasReview { get; set; }
        public List<Product> Products { get; set; }
    }
}
