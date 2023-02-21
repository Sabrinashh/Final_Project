using ColumbiaProject.DAL;
using ColumbiaProject.Models;
//using ColumbiaProject.ViewModels;
using System.Security.Claims;

namespace ColumbiaProject.Services
{
    public class LayoutService
    {
        private readonly ColumbiaDbContext _context;


        public LayoutService(ColumbiaDbContext context)
        {
            _context = context;
    
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x=>x.Key,x=>x.Value);
        }

        
    }
}
