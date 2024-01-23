using OnlineStore.OrderService.Domain.Entities;

namespace OnlineStore.OrderService.Domain.Repositories
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    }
}
