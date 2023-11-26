using MediatR;

namespace OnlineStore.CatalogService.Application.Queries
{
    public class DeleteProductByIdQuery : IRequest<bool>
    {
        public string Id { get; set; }

        public DeleteProductByIdQuery(string id)
        {
            Id = id;
        }
    }
}
