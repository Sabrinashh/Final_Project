using ColumbiaProject.Models;

namespace ColumbiaProject.ViewModels
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public Product Product { get; set; }
    }
}
