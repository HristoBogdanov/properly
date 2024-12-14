using System.ComponentModel.DataAnnotations;
using static api.Constants.PropertiesErrorMessages;
using static api.Constants.Regexes;
using static api.Constants.QueryParamsValidationConstants;

namespace api.Helpers
{
    public class PropertyQueryParams
    {
        // Filters
        [StringLength(SearchMaxLength, MinimumLength = SearchMinLength, ErrorMessage = InvalidSearchQueryLength)]
        public string? search { get; set; } = null;

        [Range(MinNumber, MaxPrice, ErrorMessage = InvalidPrice)]
        public decimal? minPrice { get; set; } = null;

        [Range(MinNumber, MaxPrice, ErrorMessage = InvalidPrice)]
        public decimal? maxPrice { get; set; } = null;

        [Range(MinNumber, MaxBathrooms, ErrorMessage = InvalidBedrooms)]
        public int? numberOfBedrooms { get; set; } = null;

        [Range(MinNumber, MaxBathrooms, ErrorMessage = InvalidBathrooms)]
        public int? numberOfBathrooms { get; set; } = null;

        [Range(MinNumber, MaxArea, ErrorMessage = InvalidArea)]
        public int? minArea { get; set; } = null;

        [Range(MinNumber, MaxArea, ErrorMessage = InvalidArea)]
        public int? maxArea { get; set; } = null;

        [Range(MinYearOfConstruction, MaxYearOfConstruction, ErrorMessage = InvalidYearOfConstruction)]
        public int? minYearOfConstruction { get; set; } = null;

        [Range(MinYearOfConstruction, MaxYearOfConstruction, ErrorMessage = InvalidYearOfConstruction)]
        public int? maxYearOfConstruction { get; set; } = null;

        public bool? forSale { get; set; } = null;

        public bool? forRent { get; set; } = null;

        public bool? isFurnished { get; set; } = null;

        // Sorting
        [RegularExpression(SortParamRegex, ErrorMessage = InvalidSortParameter)]
        public string? sortBy { get; set; } = null;

        public bool descending { get; set;} = false;

        // Pagination
        [Range(MinPage, MaxPage, ErrorMessage = InvalidPageParameter)]
        public int page { get; set; } = 1;

        [Range(MinPerPage, MaxPerPage, ErrorMessage = InvalidPerPageParameter)]
        public int perPage { get; set; } = DefaultPerPage;
    }
}