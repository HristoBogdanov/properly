using System.ComponentModel.DataAnnotations;
using static api.Constants.PropertiesErrorMessages;
using static api.Constants.PropertyValidationConstants;
using api.DTOs.Images;

namespace api.DTOs.Property
{
    public class CreatePropertyDTO
    {
        [Required(ErrorMessage = TitleRequired)]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength, ErrorMessage = InvalidTitleLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = DescriptionRequired)]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength, ErrorMessage = InvalidDescriptionLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = AddressRequired)]
        [StringLength(AddressMaxLength, MinimumLength = AddressMinLength, ErrorMessage = InvalidAddressLength)]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = PriceRequired)]
        [Range(PriceMinValue, PriceMaxValue, ErrorMessage = InvalidPrice)]
        public decimal Price { get; set; }

        [Required(ErrorMessage = ForSaleRequired)]
        public bool ForSale { get; set; }

        [Required(ErrorMessage = ForRentRequired)]
        public bool ForRent { get; set; }

        [Required(ErrorMessage = AreaRequired)]
        [Range(AreaMinValue, AreaMaxValue, ErrorMessage = InvalidArea)]
        public int Area { get; set; } = 0;

        [Required(ErrorMessage = YearOfConstructionRequired)]
        [Range(YearOfConstructionMinValue, YearOfConstructionMaxValue, ErrorMessage = InvalidYearOfConstruction)]
        public int YearOfConstruction { get; set; }

        [Range(BedroomsMinValue, BedroomsMaxValue, ErrorMessage = InvalidBedrooms)]
        public int Bedrooms { get; set; }

        [Range(BathroomsMinValue, BathroomsMaxValue, ErrorMessage = InvalidBathrooms)]
        public int Bathrooms { get; set; }

        public bool IsFurnished { get; set; }

        public string? OwnerId { get; set; }

        public virtual IEnumerable<string> Categories { get; set; } = new List<string>();

        public virtual IEnumerable<string> Features { get; set; } = new List<string>();

        public virtual IEnumerable<CreateImageDTO> Images { get; set; } = new List<CreateImageDTO>();
    }
}