using MediatR;
using OnlineStore.OrderService.Application.Responses;

namespace OnlineStore.OrderService.Application.Queries
{
    public class GetOrderListQuery : IRequest<List<OrderResponse>>
    {
        public string UserName { get; set; }

        public GetOrderListQuery(string userName)
        {
            UserName = userName;
        }
    }
}
