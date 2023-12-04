using MediatR;
using OnlineStore.BasketService.Application.Mappers;
using OnlineStore.BasketService.Application.Queries;
using OnlineStore.BasketService.Application.Responses;
using OnlineStore.BasketService.Domain.Repositories;

namespace OnlineStore.BasketService.Application.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartResponse>
    {
        private readonly IBasketRepository _basketRepository;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        public async Task<ShoppingCartResponse> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await _basketRepository.GetBasket(request.UserName);
            var shoppingCartResponse = BasketMapper.Mapper.Map<ShoppingCartResponse>(shoppingCart);
            return shoppingCartResponse!;
        }
    }
}
