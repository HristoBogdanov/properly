using System.ComponentModel.DataAnnotations;
using api.Constants;
using api.DTOs.Images;

namespace api.DTOs.Property
{
    public class CreatePropertyDTO
    {
        [Required(ErrorMessage = PropertiesErrorMessages.TitleRequired)]
        [StringLength(200, MinimumLength = 3, ErrorMessage = PropertiesErrorMessages.InvalidTitleLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = PropertiesErrorMessages.DescriptionRequired)]
        [StringLength(5000, MinimumLength = 20, ErrorMessage = PropertiesErrorMessages.InvalidDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = PropertiesErrorMessages.AddressRequired)]
        [StringLength(500, MinimumLength = 3, ErrorMessage = PropertiesErrorMessages.InvalidAddressLength)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = PropertiesErrorMessages.PriceRequired)]
        [Range(0, 100000000, ErrorMessage = PropertiesErrorMessages.InvalidPrice)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = PropertiesErrorMessages.ForSaleRequired)]
        public bool ForSale { get; set; }

        [Required(ErrorMessage = PropertiesErrorMessages.ForRentRequired)]
        public bool ForRent { get; set; }

        [Required(ErrorMessage = PropertiesErrorMessages.AreaRequired)]
        [Range(0, 10000, ErrorMessage = PropertiesErrorMessages.InvalidArea)]
        public int Area { get; set; } = 0;

        [Required(ErrorMessage = PropertiesErrorMessages.YearOfConstructionRequired)]
        [Range(1800, 2100, ErrorMessage = PropertiesErrorMessages.InvalidYearOfConstruction)]
        public int YearOfConstruction { get; set; }

        [Range(0, 100, ErrorMessage = PropertiesErrorMessages.InvalidBedrooms)]
        public int Bedrooms { get; set; }

        [Range(0, 100, ErrorMessage = PropertiesErrorMessages.InvalidBathrooms)]
        public int Bathrooms { get; set; }

        public bool IsFurnished { get; set; }

        public string OwnerId { get; set; } = null!;

        public virtual IEnumerable<string> Categories { get; set; } = new List<string>();

        public virtual IEnumerable<string> Features { get; set; } = new List<string>();

        public virtual IEnumerable<CreateImageDTO> Images { get; set; } = new List<CreateImageDTO>();
    }
}