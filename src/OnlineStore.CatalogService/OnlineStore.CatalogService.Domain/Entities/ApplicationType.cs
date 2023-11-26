using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OnlineStore.CatalogService.Domain.Entities
{
    public class ApplicationType : BaseEntity
    {
        [BsonElement("Name")]
        public string? Name { get; set; }
    }
}
