using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool? PosterStatus { get; set; }
        public Product Product { get; set; }
    }
}
