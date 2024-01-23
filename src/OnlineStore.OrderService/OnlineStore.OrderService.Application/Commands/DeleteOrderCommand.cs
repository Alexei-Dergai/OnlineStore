using MediatR;

namespace OnlineStore.OrderService.Application.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}
