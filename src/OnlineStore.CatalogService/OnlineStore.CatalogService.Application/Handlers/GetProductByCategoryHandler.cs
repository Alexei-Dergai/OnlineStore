using MediatR;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Repositories;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class GetProductByCategoryHandler : IRequestHandler<GetProductByCategoryQuery, IList<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByCategoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IList<ProductResponse>> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var productList = await _productRepository.GetProductByCategory(request.CategoryName!);
            var productListResponse = ProductMapper.Mapper.Map<IList<ProductResponse>>(productList);

            return productListResponse;
        }
    }
}
