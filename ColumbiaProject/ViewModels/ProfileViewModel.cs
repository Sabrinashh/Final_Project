using ColumbiaProject.Models;

namespace ColumbiaProject.ViewModels
{
    public class ProfileViewModel
    {
        public List<Order> Orders { get; set; } = new List<Order>();
        public MemberUpdateViewModel MemberUpdateViewModel { get; set; }
    }
}
