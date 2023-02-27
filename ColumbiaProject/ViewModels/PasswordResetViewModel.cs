using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.ViewModels
{
    public class PasswordResetViewModel
    {
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
