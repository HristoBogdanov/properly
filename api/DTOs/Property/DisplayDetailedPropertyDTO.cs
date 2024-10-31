using api.DTOs.Category;
using api.DTOs.Features;
using api.DTOs.Images;

namespace api.DTOs.Property
{
    public class DisplayDetailedPropertyDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public bool ForSale { get; set; }
        public bool ForRent { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool IsFurnished { get; set; }
        public int Area { get; set; } = 0;
        public int YearOfConstruction { get; set; }
        public Guid OwnerId { get; set; }
        public virtual ICollection<DisplayCategoryDTO> Categories { get; set; } = new List<DisplayCategoryDTO>();
        public virtual ICollection<DisplayFeatureDTO> PropertiesFeatures { get; set; } = new List<DisplayFeatureDTO>();
        public virtual ICollection<DisplayImageDTO> PropertiesImages { get; set; } = new List<DisplayImageDTO>();
    }
}