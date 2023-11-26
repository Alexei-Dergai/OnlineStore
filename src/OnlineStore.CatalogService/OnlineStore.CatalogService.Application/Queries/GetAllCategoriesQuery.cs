using MediatR;
using OnlineStore.CatalogService.Application.Responses;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetAllCategoriesQuery : IRequest<IList<CategoryResponse>>
    {

    }
}
