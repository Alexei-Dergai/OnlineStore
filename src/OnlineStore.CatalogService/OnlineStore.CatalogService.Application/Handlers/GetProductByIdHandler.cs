using MediatR;
using OnlineStore.CatalogService.Application.Exceptions;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Repositories;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            if(request.Id == null)
            {
                throw new NotFoundException("Product not found");
            }

            var product = await _productRepository.GetProductAsync(request.Id!);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(product);

            return productResponse;
        }
    }
}
