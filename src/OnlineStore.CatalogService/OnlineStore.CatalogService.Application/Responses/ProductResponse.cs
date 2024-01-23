using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Application.Responses
{
    public class ProductResponse
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public string? ImageFile { get; set; }

        public decimal Price { get; set; }

        public Category? Category { get; set; }

        public ApplicationType? ApplicationType { get; set; }
    }
}
