using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class PropertiesFeaturesConfiguration : IEntityTypeConfiguration<PropertyFeatures>
    {
        public void Configure(EntityTypeBuilder<PropertyFeatures> builder)
        {
            builder.HasKey(pf => new { pf.PropertyId, pf.FeatureId });

            builder.HasOne(pf => pf.Property)
                .WithMany(p => p.PropertiesFeatures)
                .HasForeignKey(pf => pf.PropertyId);

            builder.HasOne(pf => pf.Feature)
                .WithMany(f => f.PropertiesFeatures)
                .HasForeignKey(pf => pf.FeatureId);
        }
    }
}