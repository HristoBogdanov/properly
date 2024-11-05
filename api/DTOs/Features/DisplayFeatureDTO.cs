using api.DTOs.Images;

namespace api.DTOs.Features
{
    public class DisplayFeatureDTO
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public CreateImageDTO Image { get; set; } = null!;
    }
}