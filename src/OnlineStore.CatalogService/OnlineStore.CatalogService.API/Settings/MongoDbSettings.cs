namespace OnlineStore.CatalogService.API.Settings
{
    public class MongoDbSettings
    {
        public const string SectionName = "MongoDbSettings";

        public string? ConnectionString { get; set; }

        public string? DatabaseName { get; set; }

        public string? CollectionName { get; set; }

        public string? ApplicationTypesCollection { get; set; }

        public string? CategoriesCollection { get; set; }
    }
}
