using MediatR;
using OnlineStore.CatalogService.Application.Responses;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetProductByCategoryQuery : IRequest<IList<ProductResponse>>
    {
        public string? CategoryName { get; set; }

        public GetProductByCategoryQuery(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
