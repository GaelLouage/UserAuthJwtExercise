using AuthResourceEX.Enums;
using AuthResourceEX.Models;

namespace AuthResourceEX.Services.Interfaces
{
    public interface IJwtTokenService
    {
        TokenResponse GenerateToken(string userName, string role);
    }
}