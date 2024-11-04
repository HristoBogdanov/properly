using System.Reflection;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions)
            : base(dbContextOptions)
        {
        }

        // Enable lazy loading for all virtual properties
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Automatically apply all configurations in the project
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<PropertyCategories> PropertiesCategories { get; set; }
        public virtual DbSet<PropertyFeatures> PropertiesFeatures { get; set; }
        public virtual DbSet<PropertyImages> PropertiesImages { get; set; }
    }
}