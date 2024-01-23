using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace OnlineStore.OrderService.Infrastructure.Data
{
    public class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
    {
        public OrderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();

            optionsBuilder.UseSqlServer("Data Source=.");

            return new OrderContext(optionsBuilder.Options);
        }
    }
}
