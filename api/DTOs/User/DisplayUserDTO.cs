namespace api.DTOs.User
{
    public class DisplayUserDTO
    {
        public string Id { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public int NumberOfProperties { get; set; }
    }
}