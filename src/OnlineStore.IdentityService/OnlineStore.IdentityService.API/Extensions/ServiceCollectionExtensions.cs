using OnlineStore.IdentityService.BLL.Services.Contracts;
using OnlineStore.IdentityService.BLL.Services;
using FluentValidation;
using OnlineStore.IdentityService.BLL.Models;
using OnlineStore.IdentityService.BLL.Validators;
using OnlineStore.IdentityService.DAL.Seeder.Contracts;
using OnlineStore.IdentityService.DAL.Seeder;

namespace OnlineStore.IdentityService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServicesRegistration(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();
            services.AddScoped<IDbSeeder, DbSeeder>();
        }

        public static void AddValidatorsRegistration(this IServiceCollection services)
        {
            services.AddScoped<IValidator<LoginModel>, LoginModelValidator>();
            services.AddScoped<IValidator<RegisterModel>, RegisterModelValidator>();
        }
    }
}
