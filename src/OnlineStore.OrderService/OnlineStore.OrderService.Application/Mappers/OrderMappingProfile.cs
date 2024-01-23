using AutoMapper;
using OnlineStore.OrderService.Application.Commands;
using OnlineStore.OrderService.Application.Responses;
using OnlineStore.OrderService.Domain.Entities;

namespace OnlineStore.OrderService.Application.Mappers
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderResponse>().ReverseMap();
            CreateMap<Order, CheckoutOrderCommand>().ReverseMap();
            CreateMap<Order, UpdateOrderCommand>().ReverseMap();
        }
    }
}
