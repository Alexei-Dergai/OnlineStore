using MediatR;
using OnlineStore.CatalogService.Application.Responses;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetAllProductsQuery : IRequest<IList<ProductResponse>>
    {
    }
}
