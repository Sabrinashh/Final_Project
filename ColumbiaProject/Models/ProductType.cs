using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.Models
{
    public class ProductType
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
