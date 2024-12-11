using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static api.Constants.FeatureValidationConstants;

namespace api.Models
{
    [Table("Features")]
    public class Feature
    {
        [Key]
        [Comment("The unique identifier of the feature")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("The title of the feature")]
        public string Title { get; set; } = null!;

        [Required]
        [Comment("The unique identifier of the image of the feature")]
        public Guid ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual Image Image { get; set; } = null!;

        [Comment("Flag that indicates whether the feature is deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<PropertyFeatures> PropertiesFeatures { get; set; } = new List<PropertyFeatures>();
    }
}