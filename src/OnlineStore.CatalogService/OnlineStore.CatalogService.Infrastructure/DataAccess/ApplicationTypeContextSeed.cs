using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;
using System.Text.Json;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public static class ApplicationTypeContextSeed
    {
        public static void SeedData(IMongoCollection<ApplicationType> applicationTypeCollection)
        {
            bool checkApplicationTypes = applicationTypeCollection.Find(x => true).Any();
            string path = Path.Combine("DataAccess", "SeedData", "applicationTypes.json");

            if (!checkApplicationTypes)
            {
                var applicationTypesData = File.ReadAllText(path);
                var applicationTypes = JsonSerializer.Deserialize<List<ApplicationType>>(applicationTypesData);

                if (applicationTypes != null)
                {
                    foreach (var item in applicationTypes)
                    {
                        applicationTypeCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}
