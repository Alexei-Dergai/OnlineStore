using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategories();
    }
}