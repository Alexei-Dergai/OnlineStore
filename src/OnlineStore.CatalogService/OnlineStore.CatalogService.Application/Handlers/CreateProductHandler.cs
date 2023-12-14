using MediatR;
using OnlineStore.CatalogService.Application.Commands;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Repositories;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            var newProduct = await _productRepository.CreateProduct(productEntity);
            var productResponse = ProductMapper.Mapper.Map<ProductResponse>(newProduct);

            return productResponse;
        }
    }
}
