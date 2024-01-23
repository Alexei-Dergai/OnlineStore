using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OnlineStore.CatalogService.Domain.Entities
{
    public class Product : BaseEntity
    {
        [BsonElement("Name")]
        public string? Name { get; set; }

        public string? Summary { get; set; }

        public string? Description { get; set; }

        public string? ImageFile { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public Category? Category { get; set; }

        public ApplicationType? ApplicationType { get; set; }
    }
}
