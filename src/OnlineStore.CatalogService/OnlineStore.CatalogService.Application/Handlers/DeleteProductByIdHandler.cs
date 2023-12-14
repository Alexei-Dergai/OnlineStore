using MediatR;
using OnlineStore.CatalogService.Application.Exceptions;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Domain.Repositories;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class DeleteProductByIdHandler : IRequestHandler<DeleteProductByIdQuery, bool>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductByIdHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Handle(DeleteProductByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id == null)
            {
                throw new NotFoundException("Product not found");
            }

            return await _productRepository.DeleteProduct(request.Id);
        }
    }
}
