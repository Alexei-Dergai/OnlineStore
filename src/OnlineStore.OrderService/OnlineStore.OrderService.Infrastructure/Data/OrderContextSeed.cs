using Microsoft.Extensions.Logging;
using OnlineStore.OrderService.Domain.Entities;

namespace OnlineStore.OrderService.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
        {
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation($"Ordering Database: {typeof(OrderContext).Name} seeded.");
            }
        }

        private static IEnumerable<Order> GetOrders()
        {
            return new List<Order>
            {
            new()
            {
                UserName = "br0ken",
                FirstName = "Alexey",
                LastName = "Dergai",
                EmailAddress = "alexey.dergai@gmail.com",
                AddressLine = "Minsk",
                Country = "Belarus",
                TotalPrice = 750,
                State = "BEL",
                ZipCode = "220094",

                CardName = "MasterCard",
                CardNumber = "1234567890123456",
                CreatedBy = "Alexey",
                Expiration = "12/25",
                Cvv = "123",
                PaymentMethod = 1,
                LastModifiedBy = "Alexey",
                LastModifiedDate = new DateTime(),
            }
        };
        }
    }
}
