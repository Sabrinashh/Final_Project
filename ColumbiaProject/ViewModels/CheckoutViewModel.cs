using ColumbiaProject.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ColumbiaProject.ViewModels
{
    public class CheckoutViewModel
    {
        public Order Order { get; set; }
        public List<CheckoutItemViewModel> CheckoutItems { get; set; } = new List<CheckoutItemViewModel>();
        public decimal Total { get; set; }


    }
}
