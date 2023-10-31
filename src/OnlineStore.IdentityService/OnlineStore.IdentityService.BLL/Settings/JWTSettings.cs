namespace OnlineStore.IdentityService.BLL.Settings
{
    public class JWTSettings
    {
        public const string SectionName = "JWT";

        public string? ValidAudience { get; set; }

        public string? ValidIssuer { get ; set; }

        public string? Secret { get; set; }

        public int TokenValidityInMinutes { get; set; }

        public int RefreshTokenValidityInDays { get; set; }
    }
}
