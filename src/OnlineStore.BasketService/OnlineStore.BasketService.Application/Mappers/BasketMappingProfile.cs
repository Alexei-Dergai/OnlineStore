using AutoMapper;
using OnlineStore.BasketService.Application.Responses;
using OnlineStore.BasketService.Domain.Entities;

namespace OnlineStore.BasketService.Application.Mappers
{
    public class BasketMappingProfile : Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartResponse>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemResponse>().ReverseMap();
        }
    }
}
