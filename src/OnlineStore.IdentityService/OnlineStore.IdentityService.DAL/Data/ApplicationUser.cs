using Microsoft.AspNetCore.Identity;

namespace OnlineStore.IdentityService.DAL.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}