using MediatR;
using OnlineStore.BasketService.Application.Commands;
using OnlineStore.BasketService.Application.Mappers;
using OnlineStore.BasketService.Application.Responses;
using OnlineStore.BasketService.Domain.Entities;
using OnlineStore.BasketService.Domain.Repositories;

namespace OnlineStore.BasketService.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }

        public async Task<ShoppingCartResponse> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.UpdateBasketAsync(new ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });

            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);

            return shoppingCartResponse;
        }
    }
}
