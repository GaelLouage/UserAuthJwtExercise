using AuthResourceEX.Enums;

namespace AuthResourceEX.Models
{
    public class User
    {
        public int Id { get; set; }             // Unique identifier for the user
        public string UserName { get; set; }    // Username for login
        public string Password { get; set; } 
        public Role Role { get; set; }        // User role (e.g., "Admin", "User")
    }
}
