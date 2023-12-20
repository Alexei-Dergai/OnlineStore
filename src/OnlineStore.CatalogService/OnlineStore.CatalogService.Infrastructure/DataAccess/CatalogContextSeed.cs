using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;
using System.Text.Json;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProducts = productCollection.Find(x => true).Any();
            string path = Path.Combine("DataAccess", "SeedData", "products.json");

            if (!checkProducts)
            {
                var productsData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products != null)
                {
                    productCollection.InsertManyAsync(products);
                }
            }
        }
    }
}
