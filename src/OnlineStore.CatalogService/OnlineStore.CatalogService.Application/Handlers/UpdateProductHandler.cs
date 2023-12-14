using MediatR;
using MongoDB.Driver.Core.Operations;
using OnlineStore.CatalogService.Application.Commands;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Repositories;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, bool>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productEntity = ProductMapper.Mapper.Map<Product>(request);
            var updateProduct = await _productRepository.UpdateProduct(productEntity);
            return updateProduct;
        }
    }
}
