using MediatR;
using OnlineStore.CatalogService.Application.Responses;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetProductByNameQuery : IRequest<IList<ProductResponse>>
    {
        public string? Name { get; set; }

        public GetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
