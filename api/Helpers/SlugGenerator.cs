namespace api.Helpers
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(string title)
        {
            return title.ToLower().Replace(" ", "-");
        }
    }
}