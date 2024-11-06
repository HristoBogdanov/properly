namespace api.Constants
{
    public static class UserErrorMessages{
        public const string InvalidUsernameLength = "Invalid username length!";
        public const string UsernameRequired = "Username is required!";
        public const string InvalidPasswordLength = "Invalid password length!";
        public const string PasswordRequired = "Password is required!";
        public const string InvalidEmail = "Invalid email!";
        public const string EmailRequired = "Email is required!";
        public const string InvalidUsernameOrPassword = "Invalid username or password!";
    }

    public static class CategoryErrorMessages{
        public const string CategoryExists = "Category with that name already exists!";
        public const string TitleRequired = "Title is required!";
        public const string InvalidTitleLength = "Invalid title length!";
        public const string CategoryNotFound = "Category not found!";
    }

    public static class FeatureErrorMessages{
        public const string FeatureNotFound = "Feature not found!";
        public const string FeatureAlreadyExists = "Feature with that title already exists!";
        public const string TitleRequired = "Title is required!";
        public const string InvalidTitleLength = "Invalid title length!";
    }

    public static class CommonErrorMessages{
        public const string EntityNotFound = "Entity not found!";
        public const string InvalidServiceType = "Service type could not be obtained for the service :";
    }

    public static class ImageErrorMessages{
        public const string ImageAlreadyExists = "Image on this path already exists!";
    }

    public static class PropertiesErrorMessages{
        public const string PropertyNotFound = "Property not found!";
        public const string InvalidTitleLength = "Invalid title length!";
        public const string InvalidPrice = "Invalid price!";
        public const string InvalidArea = "Invalid area!";
        public const string InvalidYearOfConstruction = "Invalid year of construction!";
        public const string InvalidBedrooms = "Invalid number of bedrooms!";
        public const string InvalidBathrooms = "Invalid number of bathrooms!";
        public const string InvalidAddressLength = "Invalid address length!";
        public const string InvalidDescriptionLength = "Invalid description length!";
        public const string TitleRequired = "Title is required!";
        public const string AreaRequired = "Area is required!";
        public const string YearOfConstructionRequired = "Year of construction is required!";
        public const string PriceRequired = "Price is required!";
        public const string DescriptionRequired = "Description is required!";
        public const string AddressRequired = "Address is required!";
        public const string ForSaleRequired = "Mark the property as for sale or not!";
        public const string ForRentRequired = "Mark the property as for rent ot not!";
    }
}