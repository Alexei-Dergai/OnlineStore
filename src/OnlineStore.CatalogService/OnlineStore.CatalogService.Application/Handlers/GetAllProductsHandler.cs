using MediatR;
using OnlineStore.CatalogService.Application.Exceptions;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Repositories;
using OnlineStore.CatalogService.Domain.Specs;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponse>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository; 
        }

        public async Task<Pagination<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new NotFoundException("Products not found");
            }

            var productList = await _productRepository.GetAllProducts(request.CatalogSpecParams);
            var productResponseList = ProductMapper.Mapper.Map<Pagination<ProductResponse>>(productList);

            return productResponseList;
        }
    }
}
