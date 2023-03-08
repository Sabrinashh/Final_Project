using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.ViewModels
{
    public class ReviewCreateViewModel
    {
        [MaxLength(50)]
        public string Text { get; set; }
        public int ProductId { get; set; }
    }
}
