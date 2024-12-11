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
}
