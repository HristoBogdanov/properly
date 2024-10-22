using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Features")]
    public class Feature
    {
        [Key]
        [Comment("The unique identifier of the feature")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        [Comment("The title of the feature")]
        public string Title { get; set; } = String.Empty;

        [Required]
        [Comment("The path to the icon of the feature")]
        public string IconPath { get; set; } = String.Empty;

        [Comment("Flag that indicates whether the feature is deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<PropertyFeatures> PropertiesFeatures { get; set; } = new List<PropertyFeatures>();
    }
}