using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class PropertiesCategoriesConfiguration : IEntityTypeConfiguration<PropertyCategories>
    {
        public void Configure(EntityTypeBuilder<PropertyCategories> builder)
        {
            builder.HasKey(pc => new { pc.PropertyId, pc.CategoryId });

            builder.HasOne(pc => pc.Property)
                .WithMany(p => p.PropertiesCategories)
                .HasForeignKey(pc => pc.PropertyId);

            builder.HasOne(pc => pc.Category)
                .WithMany(c => c.PropertiesCategories)
                .HasForeignKey(pc => pc.CategoryId);
        }
    }
}