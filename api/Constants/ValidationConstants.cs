namespace api.Constants
{
    public static class PropertyValidationConstants
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 200;
        public const int DescriptionMinLength = 20;
        public const int DescriptionMaxLength = 5000;
        public const int AddressMinLength = 3;
        public const int AddressMaxLength = 500;
        public const int PriceMinValue = 0;
        public const int PriceMaxValue = 100000000;
        public const int AreaMinValue = 0;
        public const int AreaMaxValue = 10000;
        public const int YearOfConstructionMinValue = 1800;
        public const int YearOfConstructionMaxValue = 2100;
        public const int BedroomsMinValue = 0;
        public const int BedroomsMaxValue = 100;
        public const int BathroomsMinValue = 0;
        public const int BathroomsMaxValue = 100;
        public const int SlugMinLength = 3;
        public const int SlugMaxLength = 200;
    }

    public static class UserValidationConstants
    {
        public const int UsernameMinLength = 3;
        public const int UsernameMaxLength = 150;
        public const int PasswordMinLength = 6;
        public const int PasswordMaxLength = 100;
        public const int EmailMinLength = 3;
        public const int EmailMaxLength = 150;
    }

    public static class ImageValidationConstants
    {         
        public const int ImageNameMinLength = 3;
        public const int ImageNameMaxLength = 100;
        public const int ImagePathMinLength = 3;
        public const int ImagePathMaxLength = 1000;
    }

    public static class FeatureValidationConstants
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 200;
    }

    public static class CategoryValidationConstants
    {
        public const int TitleMinLength = 3;
        public const int TitleMaxLength = 200;
    }

    public static class QueryParamsValidationConstants
    {
        public const int SearchMinLength = 3;
        public const int SearchMaxLength = 200;
        public const int SortByMinLength = 3;
        public const int SortByMaxLength = 200;
        public const int MinNumber = 0;
        public const int MaxPrice = 100000000;
        public const int MaxBedrooms = 100;
        public const int MaxBathrooms = 100;
        public const int MaxArea = 10000;
        public const int MinYearOfConstruction = 1800;
        public const int MaxYearOfConstruction = 2100;
        public const int MinPage = 1;
        public const int MaxPage = int.MaxValue;
        public const int MinPerPage = 1;
        public const int MaxPerPage = 20;
        public const int DefaultPerPage = 10;
    }
}
