using api.DTOs.Category;
using api.DTOs.Features;
using api.DTOs.Images;

namespace api.DTOs.Property
{
    public class DisplayPropertyDTO
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public string Address { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool ForSale { get; set; }
        public bool ForRent { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }
        public bool IsFurnished { get; set; }
        public int Area { get; set; } = 0;
        public int YearOfConstruction { get; set; }
        public string OwnerId { get; set; } = null!;
        public string OwnerName { get; set; } = null!;
        public virtual IEnumerable<DisplayCategoryDTO> Categories { get; set; } = new List<DisplayCategoryDTO>();
        public virtual IEnumerable<DisplayFeatureDTO> Features { get; set; } = new List<DisplayFeatureDTO>();
        public virtual IEnumerable<CreateImageDTO> Images { get; set; } = new List<CreateImageDTO>();
    }
}