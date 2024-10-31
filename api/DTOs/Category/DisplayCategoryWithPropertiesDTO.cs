using System.ComponentModel.DataAnnotations;
using api.DTOs.Property;

namespace api.DTOs.Category
{
    public class DisplayCategoryWithPropertiesDTO
    {
        public Guid Id { get; set; }
        public string Title = null!;
        public virtual ICollection<DisplaySimplePropertyDTO> Properties { get; set; } = new List<DisplaySimplePropertyDTO>();
    }
}