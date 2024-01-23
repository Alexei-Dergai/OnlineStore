using MediatR;
using OnlineStore.BasketService.Application.Responses;

namespace OnlineStore.BasketService.Application.Queries
{
    public class GetBasketByUserNameQuery : IRequest<ShoppingCartResponse>
    {
        public string UserName { get; set; }

        public GetBasketByUserNameQuery(string userName)
        {
            UserName = userName;
        }
    }
}
