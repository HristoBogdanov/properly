using api.DTOs.Images;

namespace api.DTOs.Features
{
    public class DisplayFeatureDTO
    {
        public string Title { get; set; } = null!;
        public DisplayImageDTO Image { get; set; } = null!;
    }
}