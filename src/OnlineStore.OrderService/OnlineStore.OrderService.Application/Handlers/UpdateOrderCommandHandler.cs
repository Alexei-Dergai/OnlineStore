using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OnlineStore.OrderService.Application.Commands;
using OnlineStore.OrderService.Application.Exceptions;
using OnlineStore.OrderService.Domain.Entities;
using OnlineStore.OrderService.Domain.Repositories;

namespace OnlineStore.OrderService.Application.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateOrderCommandHandler> _logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToUpdate == null)
            {
                throw new OrderNotFoundException("Order Not Found");
            }

            _mapper.Map(request, orderToUpdate, typeof(UpdateOrderCommand), typeof(Order));

            await _orderRepository.UpdateAsync(orderToUpdate);

            _logger.LogInformation($"Order {orderToUpdate.Id} is successfully updated");

            return Unit.Value;
        }
    }
}
