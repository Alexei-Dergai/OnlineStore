using AutoMapper;

namespace OnlineStore.CatalogService.Application.Mappers
{
    public static class ProductMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.ShouldMapProperty = x => x.GetMethod!.IsPublic || x.GetMethod.IsAssembly;
                cfg.AddProfile<ProductMappingProfile>();
            });
            var mapper = config.CreateMapper();

            return mapper;
        }); 

        public static IMapper Mapper => Lazy.Value;
    }
}
