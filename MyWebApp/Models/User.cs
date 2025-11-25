namespace MyWebApp.Models
{
    public class User
    {
        public int Id { get; set; }  // Primary key
        public string Username { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
