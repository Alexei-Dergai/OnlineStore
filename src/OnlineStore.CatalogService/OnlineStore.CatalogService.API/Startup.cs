using MediatR;
using Microsoft.OpenApi.Models;
using OnlineStore.CatalogService.API.Extensions;
using OnlineStore.CatalogService.API.Middlewares;
using OnlineStore.CatalogService.API.Settings;
using OnlineStore.CatalogService.Application.Handlers;
using OnlineStore.CatalogService.Domain.Repositories;
using OnlineStore.CatalogService.Infrastructure.DataAccess;
using OnlineStore.CatalogService.Infrastructure.DataAccess.Contracts;
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
            services.Configure<MongoDbSettings>(_configuration.GetSection(MongoDbSettings.SectionName));

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Catalog.API", Version = "v1" }); });
            services.AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });

            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddHealthChecksRegistration(_configuration);
            services.AddControllers();
            services.AddApiVersioning();
            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(CreateProductHandler).GetTypeInfo().Assembly);

            services.AddDbContextRegistration(_configuration);

            services.AddScoped<IDbSeeder, DbSeeder>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, ProductRepository>();
            services.AddScoped<IApplicationTypeRepository, ProductRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbSeeder dbSeeder)
        {
            dbSeeder.Seed();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catalog.API v1"));
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.AddEndPointsRegistration();
        }
    }
}
