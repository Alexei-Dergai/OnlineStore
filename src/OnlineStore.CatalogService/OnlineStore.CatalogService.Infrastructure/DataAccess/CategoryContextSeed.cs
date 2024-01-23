using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public static class CategoryContextSeed
    {
        public static void SeedData(IMongoCollection<Category> categoryCollection)
        {
            bool checkCategories = categoryCollection.Find(x => true).Any();

            if (!checkCategories)
            {
                categoryCollection.InsertManyAsync(DataForSeeding.GetCategories());
            }
        }
    }
}
