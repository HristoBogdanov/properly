using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("PropertiesImages")]
    public class PropertyImages
    {
        [Required]
        [Comment("Inuqie identifier of the property")]
        public Guid PropertyId { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public virtual Property Property { get; set; } = null!;

        [Required]
        [Comment("Inuqie identifier of the image")]
        public Guid ImageId { get; set; }

        [ForeignKey(nameof(ImageId))]
        public virtual Image Image { get; set; } = null!;
    }
}