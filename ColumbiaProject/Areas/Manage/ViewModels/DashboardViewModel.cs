using ColumbiaProject.Models;

namespace ColumbiaProject.Areas.Manage.ViewModels
{
    public class DashboardViewModel
    {
        public List<AppUser> Users { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public List<Order> Orders { get; set; }

        public List<ProductType> ProductTypes { get; set; }
    }
}
