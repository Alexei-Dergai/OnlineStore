namespace OnlineStore.IdentityService.DAL.Data
{
    public class ApplicationUserExtended : ApplicationUser
    {
        public string? Role { get; set; }

        public string? Password { get; set; }
    }
}
