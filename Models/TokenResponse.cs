namespace AuthResourceEX.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }      // JWT token
        public DateTime Expiration { get; set; } // Token expiration time
    }
}
