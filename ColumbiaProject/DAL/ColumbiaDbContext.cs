using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ColumbiaProject.Models;

namespace ColumbiaProject.DAL
{
   
        public class ColumbiaDbContext:IdentityDbContext
        {
            public ColumbiaDbContext(DbContextOptions<ColumbiaDbContext> options) : base(options)
            {

            }

        public DbSet<Header> Headers { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
    }

