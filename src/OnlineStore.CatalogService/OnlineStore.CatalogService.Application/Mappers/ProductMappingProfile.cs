using AutoMapper;
using OnlineStore.CatalogService.Application.Commands;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductResponse>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<ApplicationType, ApplicationTypeResponse>().ReverseMap();
        }
    }
}
