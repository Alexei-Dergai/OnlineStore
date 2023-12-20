using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;
using System.Text.Json;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public static class CategoryContextSeed
    {
        public static void SeedData(IMongoCollection<Category> categoryCollection)
        {
            bool checkCategories = categoryCollection.Find(x => true).Any();
            string path = Path.Combine("DataAccess", "SeedData", "categories.json");

            if (!checkCategories)
            {
                var categoriesData = File.ReadAllText(path);
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);

                if (categories != null)
                {
                    categoryCollection.InsertManyAsync(categories);
                }
            }
        }
    }
}
