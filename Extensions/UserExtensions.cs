using AuthResourceEX.Enums;
using AuthResourceEX.Models;

namespace AuthResourceEX.Extensions
{
    public static class UserExtensions
    {
        public static Role GetRole(this string role)
        {
            return role switch
            {
                "ADMIN" => Role.ADMIN,
                "SUPERADMIN" => Role.SUPERADMIN,
                _ => Role.USER,
            };
        }
    }
}
