using OnlineStore.IdentityService.BLL.Services.Contracts;
using OnlineStore.IdentityService.BLL.Services;

namespace OnlineStore.IdentityService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesRegistration(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
        }
    }
}
