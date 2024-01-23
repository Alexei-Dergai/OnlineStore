using OnlineStore.CatalogService.Infrastructure.DataAccess.Contracts;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public class DbSeeder : IDbSeeder
    {
        private readonly ICatalogContext _catalogContext;

        public DbSeeder(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;   
        }

        public void Seed()
        {
            ApplicationTypeContextSeed.SeedData(_catalogContext.ApplicationTypes);
            CategoryContextSeed.SeedData(_catalogContext.Categories);
            CatalogContextSeed.SeedData(_catalogContext.Products);
        }
    }
}
