using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public static class ApplicationTypeContextSeed
    {
        public static void SeedData(IMongoCollection<ApplicationType> applicationTypeCollection)
        {
            bool checkApplicationTypes = applicationTypeCollection.Find(x => true).Any();

            if (!checkApplicationTypes)
            {
                applicationTypeCollection.InsertManyAsync(DataForSeeding.GetApplicationTypes());
            }
        }
    }
}
