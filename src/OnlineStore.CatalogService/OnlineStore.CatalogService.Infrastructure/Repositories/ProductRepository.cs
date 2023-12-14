using MongoDB.Bson;
using MongoDB.Driver;
using OnlineStore.CatalogService.Domain.Entities;
using OnlineStore.CatalogService.Domain.Repositories;
using OnlineStore.CatalogService.Domain.Specs;
using OnlineStore.CatalogService.Infrastructure.DataAccess;

namespace OnlineStore.CatalogService.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IApplicationTypeRepository, ICategoryRepository
    {
        private readonly ICatalogContext _context;

        public ProductRepository(ICatalogContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);

            return product;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Id, id);

            var deleteResult = await _context.Products.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<ApplicationType>> GetAllApplicationTypesAsync()
        {
            return await _context.ApplicationTypes.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Find(x => true).ToListAsync();
        }

        public async Task<Pagination<Product>> GetAllProductsAsync(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;

            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                var searchFilter = builder.Regex(x=> x.Name, new BsonRegularExpression(catalogSpecParams.Search));

                filter &= searchFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.CategoryId))
            {
                var categoryFilter = builder.Eq(x => x.Category!.Id, catalogSpecParams.CategoryId);

                filter &= categoryFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.ApplicationTypeId))
            {
                var applicationTypeFilter = builder.Eq(x => x.ApplicationType!.Id, catalogSpecParams.ApplicationTypeId);

                filter &= applicationTypeFilter;
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                return new Pagination<Product>
                {
                    PageSize = catalogSpecParams.PageSize,
                    PageIndex = catalogSpecParams.PageIndex,
                    Data = await DataFilter(catalogSpecParams, filter),
                    Count = await _context.Products.CountDocumentsAsync(p =>
                        true) //TODO: Need to check while applying with UI
                };
            }
            return new Pagination<Product>
            {
                PageSize = catalogSpecParams.PageSize,
                PageIndex = catalogSpecParams.PageIndex,
                Data = await _context
                    .Products
                    .Find(filter)
                    .Sort(Builders<Product>.Sort.Ascending("Name"))
                    .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                    .Limit(catalogSpecParams.PageSize)
                    .ToListAsync(),
                Count = await _context.Products.CountDocumentsAsync(p => true)
            };
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            switch (catalogSpecParams.Sort)
            {
                case "priceAsc":
                    return await _context
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Ascending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                case "priceDesc":
                    return await _context
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Descending("Price"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
                default:
                    return await _context
                        .Products
                        .Find(filter)
                        .Sort(Builders<Product>.Sort.Ascending("Name"))
                        .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                        .Limit(catalogSpecParams.PageSize)
                        .ToListAsync();
            }
        }

        public async Task<Product> GetProductAsync(string id)
        {
            return await _context.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Category!.Name, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(x => x.Name, name);

            return await _context.Products.Find(filter).ToListAsync();
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            var updateResult = await _context.Products.ReplaceOneAsync(x => x.Id == product.Id, product);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
