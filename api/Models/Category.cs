using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [Comment("The unique identifier of the category")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        [Comment("The title of the category")]
        public string Title { get; set; } = String.Empty;

        [Comment("Flag that indicates whether the category is deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<PropertyCategories> PropertiesCategories { get; set; } = new List<PropertyCategories>();
    }
}