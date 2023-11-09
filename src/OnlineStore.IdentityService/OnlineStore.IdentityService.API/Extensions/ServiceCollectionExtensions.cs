using OnlineStore.IdentityService.BLL.Services.Contracts;
using OnlineStore.IdentityService.BLL.Services;
using FluentValidation;
using OnlineStore.IdentityService.BLL.Validators;
using OnlineStore.IdentityService.DAL.Seeder.Contracts;
using OnlineStore.IdentityService.DAL.Seeder;
using Microsoft.AspNetCore.Identity;
using OnlineStore.IdentityService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using OnlineStore.IdentityService.BLL.Settings;
using System.Text;

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
            services.AddValidatorsFromAssemblyContaining<LoginModelValidator>();
        }

        public static void AddDbContextRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var jwtSettings = new JWTSettings();

                configuration.Bind(JWTSettings.SectionName, jwtSettings);

                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidAudience = jwtSettings.ValidAudience,
                    ValidIssuer = jwtSettings.ValidIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret!))
                };
            });
        }
    }
}
