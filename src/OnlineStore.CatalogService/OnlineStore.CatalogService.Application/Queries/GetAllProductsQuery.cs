using MediatR;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Specs;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetAllProductsQuery : IRequest<Pagination<ProductResponse>>
    {
        public CatalogSpecParams CatalogSpecParams { get; set; }

        public GetAllProductsQuery(CatalogSpecParams catalogSpecParams)
        {
            CatalogSpecParams = catalogSpecParams;
        }
    }
}
