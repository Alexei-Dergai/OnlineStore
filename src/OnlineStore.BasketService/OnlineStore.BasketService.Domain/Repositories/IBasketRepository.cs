using OnlineStore.BasketService.Domain.Entities;

namespace OnlineStore.BasketService.Domain.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasketAsync(string userName);
        Task<ShoppingCart> UpdateBasketAsync(ShoppingCart shoppingCart);
        Task DeleteBasketAsync(string userName);
    }
}
