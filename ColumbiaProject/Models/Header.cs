using ColumbiaProject.Attributes.ValidationAttributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ColumbiaProject.Models
{
    public class Header
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string? Title { get; set; }
        [MaxLength(150)]
        public string? Subtitle { get; set; }
        public string Image { get; set; }
        [NotMapped]
        [MaxFileSize(2)]
        [AllowedFileTypes("image/png", "image/jpeg")]
        public IFormFile? ImageFile { get; set; }
    }
}
