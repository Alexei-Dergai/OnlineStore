using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Application.Responses
{
    public class ProductResponse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

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
