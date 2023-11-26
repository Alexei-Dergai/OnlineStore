using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Domain.Repositories
{
    public interface IApplicationTypeRepository
    {
        Task<IEnumerable<ApplicationType>> GetAllApplicationTypes();
    }
}
