using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
        IMongoCollection<ApplicationType> ApplicationTypes { get; }
        IMongoCollection<Category> Categories { get; }
    }
}
