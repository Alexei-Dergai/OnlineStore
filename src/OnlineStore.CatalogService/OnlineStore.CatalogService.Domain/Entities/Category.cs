using System.ComponentModel.DataAnnotations;

namespace OnlineStore.CatalogService.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string? Name { get; set; }
    }
}
