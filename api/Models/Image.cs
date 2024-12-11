using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static api.Constants.ImageValidationConstants;

namespace api.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        [Comment("The unique identifier for the image.")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(ImageNameMaxLength)]
        [Comment("The name of the image.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(ImagePathMaxLength)]
        [Comment("The path to the image.")]
        public string Path { get; set; } = null!;

        public virtual ICollection<PropertyImages> PropertiesImages { get; set; } = new List<PropertyImages>();
    }
}