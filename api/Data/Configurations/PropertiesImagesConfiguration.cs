
using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class PropertiesImagesConfiguration : IEntityTypeConfiguration<PropertyImages>
    {
        public void Configure(EntityTypeBuilder<PropertyImages> builder)
        {
            builder.HasKey(pi => new { pi.PropertyId, pi.ImageId });

            builder.HasOne(pi => pi.Property)
                .WithMany(p => p.PropertiesImages)
                .HasForeignKey(pi => pi.PropertyId);

            builder.HasOne(pi => pi.Image)
                .WithMany(i => i.PropertiesImages)
                .HasForeignKey(pi => pi.ImageId);
        }
    }
}