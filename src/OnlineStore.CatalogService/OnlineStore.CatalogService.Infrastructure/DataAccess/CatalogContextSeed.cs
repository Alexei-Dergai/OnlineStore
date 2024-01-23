using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProducts = productCollection.Find(x => true).Any();

            if (!checkProducts)
            {
                productCollection.InsertManyAsync(DataForSeeding.GetProducts());
            }
        }
    }
}
