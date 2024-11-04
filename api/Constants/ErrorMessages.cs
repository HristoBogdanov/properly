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

    public static class CommonErrorMessages{
        public const string EntityNotFound = "Entity not found!";
        public const string InvalidServiceType = "Service type could not be obtained for the service :";
    }
}