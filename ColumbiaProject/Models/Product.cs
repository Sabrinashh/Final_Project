using ColumbiaProject.Attributes.ValidationAttributes;
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
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
        public ProductType? ProductType { get; set; }
        public int ProductTypeId { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public decimal SalePrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal CostPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPercent { get; set; }
        public bool IsSpecial { get; set; }
        public bool IsNew { get; set; }
        public bool IsSold { get; set; }
        public bool StockStatus { get; set; }
        public bool IsDeleted { get; set; }
        public List<ProductImage>? ProductImages { get; set; }
        public List<ProductSize>? ProductSizes { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public IFormFile? PosterFile { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/jpeg", "image/png")]
        public List<IFormFile>? ImageFiles { get; set; } = new List<IFormFile>();
        [NotMapped]
        public List<int>? ProductImageIds { get; set; } = new List<int>();
        [NotMapped]
        public List<int>? SizeIds { get; set; } = new List<int>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow.AddHours(4);
        public List<Review>? Reviews { get; set; }
     

    }
}
