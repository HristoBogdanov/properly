using api.DTOs.Images;
using api.DTOs.Property;

namespace api.DTOs.Features
{
    public class DisplayFeatureWithPropertiesDTO
    {
        public string Title { get; set; } = null!;
        public CreateImageDTO Image { get; set; } = null!;
        public virtual ICollection<DisplaySimplePropertyDTO> Properties { get; set; } = new List<DisplaySimplePropertyDTO>();
    }
}