using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using OnlineStore.CatalogService.API.Settings;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Infrastructure.DataAccess;
using OnlineStore.CatalogService.Infrastructure.DataAccess.Contracts;

namespace OnlineStore.CatalogService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecksRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                    .AddMongoDb(configuration["DatabaseSettings:ConnectionString"]!, "Catalog Mongo Db Health Check",
                                HealthStatus.Degraded);
        }

        public static void AddEndPointsRegistration(this IApplicationBuilder app)
        {
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

        public static void AddDbContextRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICatalogContext, CatalogContext>(serviceProvider =>
            {
                var mongoDbSettings = new MongoDbSettings();

                configuration.Bind(MongoDbSettings.SectionName, mongoDbSettings);

                var client = new MongoClient(mongoDbSettings.ConnectionString);
                var database = client.GetDatabase(mongoDbSettings.DatabaseName);

                var applicationTypes = database.GetCollection<ApplicationType>(mongoDbSettings.ApplicationTypesCollection);
                var categories = database.GetCollection<Category>(mongoDbSettings.CategoriesCollection);
                var products = database.GetCollection<Product>(mongoDbSettings.CollectionName);

                return new CatalogContext
                {
                    ApplicationTypes = applicationTypes,
                    Categories = categories,
                    Products = products
                };
            });
        }
    }
}
