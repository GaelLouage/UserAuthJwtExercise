using AuthResourceEX.Models;

namespace AuthResourceEX.Data
{
    public static class Users
    {
        public static List<User> UsersList = new List<User>
        {
            new User
            {
                Id = 1,
                UserName = "johndoe",
                Password = "Password1", // In a real scenario, this would be securely hashed
                Role =  Enums.Role.SUPERADMIN
            },
            new User
            {
                Id = 2,
                UserName = "janedoe",
                Password = "Password2",
                Role =  Enums.Role.ADMIN
            },
            new User
            {
                Id = 3,
                UserName = "alice",
                Password = "Password3",
                 Role =  Enums.Role.USER
            }
        };
    }
}
