using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Specs;

namespace OnlineStore.CatalogService.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Pagination<Product>> GetAllProductsAsync(CatalogSpecParams catalogSpecParams);

        Task<Product> GetProductAsync(string id);

        Task<IEnumerable<Product>> GetProductByNameAsync(string name);

        Task<IEnumerable<Product>> GetProductByCategoryAsync(string name);

        Task<Product> CreateProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(string id);

    }
}
