using MediatR;
using OnlineStore.BasketService.Application.Exceptions;
using OnlineStore.BasketService.Application.Queries;
using OnlineStore.BasketService.Domain.Repositories;

namespace OnlineStore.BasketService.Application.Handlers
{
    public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameQuery>
    {
        private readonly IBasketRepository _basketRepository;

        public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<Unit> Handle(DeleteBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            if (request.UserName == null)
            {
                throw new NotFoundException("Product not found");
            }

            await _basketRepository.DeleteBasketAsync(request.UserName);

            return Unit.Value;
        }
    }
}
