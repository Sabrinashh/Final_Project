using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [MaxLength(100)]
        public string Email { get; set; }
    }
}
