using MediatR;
using OnlineStore.BasketService.Application.Responses;
using OnlineStore.BasketService.Domain.Entities;

namespace OnlineStore.BasketService.Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartResponse>
    {
        public CreateShoppingCartCommand(string userName, List<ShoppingCartItem> items)
        {
            UserName = userName;
            Items = items;
        }
        public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
    }
}
