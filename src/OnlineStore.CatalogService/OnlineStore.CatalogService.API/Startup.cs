using HealthChecks.UI.Client;
using MediatR;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using OnlineStore.CatalogService.Application.Handlers;
using OnlineStore.CatalogService.Domain.Repositories;
using OnlineStore.CatalogService.Infrastructure.DataAccess;
using OnlineStore.CatalogService.Infrastructure.Repositories;
using System.Reflection;

namespace OnlineStore.CatalogService.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHealthChecks()
                    .AddMongoDb(_configuration["DatabaseSettings:ConnectionString"]!, "Catalog Mongo Db Health Check",
                                HealthStatus.Degraded);

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, ProductRepository>();
            services.AddScoped<IApplicationTypeRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            });
        }
    }
}
