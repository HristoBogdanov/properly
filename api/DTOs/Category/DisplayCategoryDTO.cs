using System.ComponentModel.DataAnnotations;

namespace api.DTOs.Category
{
    public class DisplayCategoryDTO
    {
        public Guid Id;
        public string Title = null!;
    }
}