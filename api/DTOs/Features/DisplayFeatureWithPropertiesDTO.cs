using api.DTOs.Images;
using api.DTOs.Property;

namespace api.DTOs.Features
{
    public class DisplayFeatureWithPropertiesDTO
    {
        public string Title { get; set; } = null!;
        public DisplayImageDTO Image { get; set; } = null!;
        public virtual ICollection<DisplaySimplePropertyDTO> Properties { get; set; } = new List<DisplaySimplePropertyDTO>();
    }
}