using System.ComponentModel.DataAnnotations;
using api.Constants;

namespace api.Helpers
{
    public class PropertyQueryParams
    {
        // Filters
        [StringLength(200, MinimumLength = 3, ErrorMessage = PropertiesErrorMessages.InvalidSearchQueryLength)]
        public string? search { get; set; } = null;

        [Range(0, 100000000, ErrorMessage = PropertiesErrorMessages.InvalidPrice)]
        public decimal? minPrice { get; set; } = null;

        [Range(0, 100000000, ErrorMessage = PropertiesErrorMessages.InvalidPrice)]
        public decimal? maxPrice { get; set; } = null;

        [Range(0, 100, ErrorMessage = PropertiesErrorMessages.InvalidBedrooms)]
        public int? numberOfBedrooms { get; set; } = null;

        [Range(0, 100, ErrorMessage = PropertiesErrorMessages.InvalidBathrooms)]
        public int? numberOfBathrooms { get; set; } = null;

        [Range(0, 10000, ErrorMessage = PropertiesErrorMessages.InvalidArea)]
        public int? minArea { get; set; } = null;

        [Range(0, 10000, ErrorMessage = PropertiesErrorMessages.InvalidArea)]
        public int? maxArea { get; set; } = null;

        [Range(1800, 2100, ErrorMessage = PropertiesErrorMessages.InvalidYearOfConstruction)]
        public int? minYearOfConstruction { get; set; } = null;

        [Range(1800, 2100, ErrorMessage = PropertiesErrorMessages.InvalidYearOfConstruction)]
        public int? maxYearOfConstruction { get; set; } = null;

        public bool? forSale { get; set; } = null;

        public bool? forRent { get; set; } = null;

        public bool? isFurnished { get; set; } = null;

        // Sorting
        [RegularExpression(Regexes.SortParamRegex, ErrorMessage = PropertiesErrorMessages.InvalidSortParameter)]
        public string? sortBy { get; set; } = null;

        public bool descending { get; set;} = false;

        // Pagination
        [Range(1, int.MaxValue, ErrorMessage = PropertiesErrorMessages.InvalidPageParameter)]
        public int page { get; set; } = 1;

        [Range(1, 20, ErrorMessage = PropertiesErrorMessages.InvalidPerPageParameter)]
        public int perPage { get; set; } = 10;
    }
}