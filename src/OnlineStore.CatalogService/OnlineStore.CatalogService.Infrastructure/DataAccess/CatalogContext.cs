using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Infrastructure.DataAccess.Contracts;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; set; }
        public IMongoCollection<ApplicationType> ApplicationTypes { get; set; }
        public IMongoCollection<Category> Categories { get; set; }
    }
}
