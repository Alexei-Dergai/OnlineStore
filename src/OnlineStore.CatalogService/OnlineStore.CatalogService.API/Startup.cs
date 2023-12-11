using MediatR;
using Microsoft.OpenApi.Models;
using OnlineStore.CatalogService.API.Extensions;
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
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddHealthChecksRegistration(_configuration);
            builder.Services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });

            services.AddHealthChecks();
            services.AddControllers();
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
            app.AddEndPointsRegistration();
        }
    }
}
