namespace OnlineStore.IdentityService.BLL.Models
{
    public class AuthenticationResult
    {
        public string? Token { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime Expiration { get; set; }
    }
}
