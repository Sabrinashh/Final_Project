using ColumbiaProject.Models;

namespace ColumbiaProject.ViewModels
{
    public class HomeViewModel
    {

        public List<Header> Headers { get; set; }
        public Dictionary<string, string> Settings { get; set; }
        public List<Product> Products { get; set; }
    }
}
