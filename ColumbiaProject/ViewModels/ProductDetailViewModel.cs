using ColumbiaProject.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

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
