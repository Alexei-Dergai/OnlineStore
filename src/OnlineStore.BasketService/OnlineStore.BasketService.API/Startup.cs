using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using OnlineStore.BasketService.API.Extensions;
using OnlineStore.BasketService.API.Middlewares;
using OnlineStore.BasketService.Application.Handlers;
using OnlineStore.BasketService.Domain.Repositories;
using OnlineStore.BasketService.Infrastructure.Repositories;
using System.Reflection;

namespace OnlineStore.BasketService.API
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();
            services.AddHealthChecksRegistration(Configuration);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = Configuration.GetValue<string>("CacheSettings:ConnectionString");
            });

            services.AddMediatR(typeof(CreateShoppingCartCommandHandler).GetTypeInfo().Assembly);

            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddScoped<IBasketRepository, BasketRepository>();

            services.AddAutoMapper(typeof(Startup));

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService.API", Version = "v1" });
            });

            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.AddEndPointsRegistration();

        }
    }
}
