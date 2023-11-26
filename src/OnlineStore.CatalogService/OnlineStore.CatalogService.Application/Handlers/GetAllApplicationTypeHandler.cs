using MediatR;
using OnlineStore.CatalogService.Application.Mappers;
using OnlineStore.CatalogService.Application.Queries;
using OnlineStore.CatalogService.Application.Responses;
using OnlineStore.CatalogService.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore.CatalogService.Application.Handlers
{
    public class GetAllApplicationTypeHandler : IRequestHandler<GetAllApplicationTypeQuery, IList<ApplicationTypeResponse>>
    {
        private readonly IApplicationTypeRepository _applicationTypeRepository;

        public GetAllApplicationTypeHandler(IApplicationTypeRepository applicationTypeRepository)
        {
            _applicationTypeRepository = applicationTypeRepository;
        }

        public async Task<IList<ApplicationTypeResponse>> Handle(GetAllApplicationTypeQuery request, CancellationToken cancellationToken)
        {
            var applicationTypeList = await _applicationTypeRepository.GetAllApplicationTypes();
            var applicationTypeResponseList = ProductMapper.Mapper.Map<IList<ApplicationTypeResponse>>(applicationTypeList);
            
            return applicationTypeResponseList;
        }
    }
}
