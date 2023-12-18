using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OnlineStore.CatalogService.Application.Settings;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public IMongoCollection<ApplicationType> ApplicationTypes { get; }
        public IMongoCollection<Category> Categories { get; }

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            ApplicationTypes = database.GetCollection<ApplicationType>(
                configuration.GetValue<string>("DatabaseSettings:ApplicationTypesCollection"));
            Categories = database.GetCollection<Category>(
                configuration.GetValue<string>("DatabaseSettings:CategoriesCollection"));
            Products = database.GetCollection<Product>(
                configuration.GetValue<string>("DatabaseSettings:CollectionName"));

            ApplicationTypeContextSeed.SeedData(ApplicationTypes);
            CategoryContextSeed.SeedData(Categories);
            CatalogContextSeed.SeedData(Products);
        }
    }
}
