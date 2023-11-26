using MediatR;
using OnlineStore.CatalogService.Application.Responses;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class GetAllApplicationTypeQuery : IRequest<IList<ApplicationTypeResponse>>
    {

    }
}
