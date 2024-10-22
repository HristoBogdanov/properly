using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("PropertiesFeatures")]
    public class PropertyFeatures
    {
        [Required]
        [Comment("Unique identifier for the property")]
        public Guid PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;

        [Required]
        [Comment("Unique identifier for the feature")]
        public Guid FeatureId { get; set; }

        [ForeignKey(nameof(FeatureId))]
        public virtual Feature Feature { get; set; } = null!;
    }
}