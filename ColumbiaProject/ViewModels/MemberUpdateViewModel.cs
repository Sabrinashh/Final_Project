using ColumbiaProject.Models;
using System.ComponentModel.DataAnnotations;

namespace ColumbiaProject.ViewModels
{
    public class MemberUpdateViewModel
    {
        [MaxLength(25)]
        public string Username { get; set; }
        [MaxLength(25)]
        public string Fullname { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        public string?Password { get; set; }
        [MaxLength(20)]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        public string?ConfirmPassword { get; set; }
        public List<Order>? Orders { get; set; }
    }
}
