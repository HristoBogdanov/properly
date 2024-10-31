using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Images")]
    public class Image
    {
        [Key]
        [Comment("The unique identifier for the image.")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        [Comment("The name of the image.")]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        [Comment("The path to the image.")]
        public string Path { get; set; } = null!;
    }
}