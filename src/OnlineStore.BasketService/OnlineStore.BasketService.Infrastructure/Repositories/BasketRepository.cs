using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using OnlineStore.BasketService.Domain.Entities;
using OnlineStore.BasketService.Domain.Repositories;

namespace OnlineStore.BasketService.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }
        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
            {
                return null!;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket)!;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            await _redisCache.SetStringAsync(shoppingCart.UserName!, JsonConvert.SerializeObject(shoppingCart));
            return await GetBasket(shoppingCart.UserName!);
        }

        public async Task DeleteBasket(string userName)
        {
            await _redisCache.RemoveAsync(userName);
        }
    }
}