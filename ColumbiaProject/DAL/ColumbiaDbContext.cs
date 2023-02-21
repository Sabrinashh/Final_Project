using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ColumbiaProject.Models;

namespace ColumbiaProject.DAL
{
   
        public class ColumbiaDbContext : IdentityDbContext
        {
            public ColumbiaDbContext(DbContextOptions<ColumbiaDbContext> options) : base(options)
            {

            }

        // public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        // public DbSet<Category> Categories { get; set; }
        // public DbSet<AppUser> AppUsers { get; set; }
        //public DbSet <Comment> Comments { get; set; }
    }
    }

