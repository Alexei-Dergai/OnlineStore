using MediatR;
using OnlineStore.CatalogService.Application.Exceptions;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Repositories;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery, IList<CategoryResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetAllCategoriesHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IList<CategoryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            if (_categoryRepository.GetAllCategories == null)
            {
                throw new NotFoundException("Categories not found");
            }

            var categoryList = await _categoryRepository.GetAllCategories();
            var categoryResponseList = ProductMapper.Mapper.Map<IList<Category>,
                                                                IList<CategoryResponse>>(categoryList.ToList());

            return categoryResponseList;
        }
    }
}
