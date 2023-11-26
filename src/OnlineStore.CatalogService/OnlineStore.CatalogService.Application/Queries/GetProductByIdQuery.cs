using MediatR;
using OnlineStore.CatalogService.Application.Responses;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetProductByIdQuery : IRequest<ProductResponse>
    {
        public string? Id { get; set; }

        public GetProductByIdQuery(string id)
        {
            Id = id;
        }

    }
}
