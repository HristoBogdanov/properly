using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using static api.Constants.PropertyValidationConstants;

namespace api.Models
{
    [Table("Properties")]
    public class Property
    {
        [Key]
        [Comment("The unique identifier of the property")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(TitleMaxLength)]
        [Comment("The title of the property")]
        public string Title { get; set; } = String.Empty;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        [Comment("The description of the property")]
        public string Description { get; set; } = String.Empty;

        [Required]
        [MaxLength(SlugMaxLength)]
        [Comment("The slug of the property")]
        public string Slug { get; set; } = String.Empty;

        [Required]
        [MaxLength(AddressMaxLength)]
        [Comment("The address of the property")]
        public string Address { get; set; } = String.Empty;

        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        [Comment("The price of the property")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [Comment("Flag that indicates if the property is for sale")]
        public bool ForSale { get; set; }

        [Required]
        [Comment("Flag that indicates if the property is for rent")]
        public bool ForRent { get; set; }

        [Comment("The number of bedrooms of the property")]
        public int Bedrooms { get; set; }

        [Comment("The number of bathrooms of the property")]
        public int Bathrooms { get; set; }

        [Comment("Flag that indicates if the property is furnished")]
        public bool IsFurnished { get; set; }

        [Required]
        [Comment("The total area of the property in square meters")]
        public int Area { get; set; } = 0;

        [Required]
        [Comment("The year of construction of the property")]
        public int YearOfConstruction { get; set; } = 2000;

        [Required]
        [Comment("The unique identifier of the property owner")]
        public Guid OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public virtual ApplicationUser Owner { get; set; } = null!;

        [Comment("Flag that indicates whether the property is deleted")]
        public bool IsDeleted { get; set; } = false;

        public virtual ICollection<PropertyCategories> PropertiesCategories { get; set; } = new List<PropertyCategories>();
        public virtual ICollection<PropertyFeatures> PropertiesFeatures { get; set; } = new List<PropertyFeatures>();
        public virtual ICollection<PropertyImages> PropertiesImages { get; set; } = new List<PropertyImages>();
    }
}