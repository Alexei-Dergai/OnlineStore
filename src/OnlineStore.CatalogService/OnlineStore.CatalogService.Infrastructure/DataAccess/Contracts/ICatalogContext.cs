using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess.Contracts
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; set; }
        IMongoCollection<ApplicationType> ApplicationTypes { get; set; }
        IMongoCollection<Category> Categories { get; set; }
    }
}
