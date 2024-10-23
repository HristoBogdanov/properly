using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {

        private readonly IConfiguration _configuration;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> dbContextOptions, 
            IConfiguration configuration)
            : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        // Enable lazy loading for all virtual properties
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Adding the roles to the database
            List<IdentityRole<Guid>> roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "Broker",
                    NormalizedName = "BROKER"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "USER"
                },
            };
            builder.Entity<IdentityRole<Guid>>().HasData(roles);
            

            // Configurting the many-to-many relationship between properties and categories
            builder.Entity<PropertyCategories>()
                .HasKey(pc => new { pc.PropertyId, pc.CategoryId });

            builder.Entity<PropertyCategories>()
                .HasOne(pc => pc.Property)
                .WithMany(p => p.PropertiesCategories)
                .HasForeignKey(pc => pc.PropertyId);

            builder.Entity<PropertyCategories>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PropertiesCategories)
                .HasForeignKey(pc => pc.CategoryId);

            // Configurting the many-to-many relationship between properties and features
            builder.Entity<PropertyFeatures>()
                .HasKey(pf => new { pf.PropertyId, pf.FeatureId});

            builder.Entity<PropertyFeatures>()
                .HasOne(pf => pf.Property)
                .WithMany(p => p.PropertiesFeatures)
                .HasForeignKey(pf => pf.PropertyId);

            builder.Entity<PropertyFeatures>()
                .HasOne(pf => pf.Feature)
                .WithMany(f => f.PropertiesFeatures)
                .HasForeignKey(pf => pf.FeatureId);
        }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PropertyCategories> PropertiesCategories { get; set; }
        public DbSet<PropertyFeatures> PropertiesFeatures { get; set; }
    }
}