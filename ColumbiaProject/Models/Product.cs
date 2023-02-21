using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColumbiaProject.Models
{
    public class Product
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public string Category { get; set; }
        public int CategoryId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNew { get; set; }
        public bool IsSold { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }

    }
}
