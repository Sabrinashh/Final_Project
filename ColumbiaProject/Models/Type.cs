using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.Models
{
    public class Type
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
