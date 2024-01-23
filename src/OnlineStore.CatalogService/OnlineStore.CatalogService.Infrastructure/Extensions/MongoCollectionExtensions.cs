using MongoDB.Driver;

namespace OnlineStore.CatalogService.Infrastructure.Extensions
{
    public static class MongoCollectionExtensions
    {
        public static IFindFluent<TDocument, TProjection> AddPagination<TDocument, TProjection>(
            this IFindFluent<TDocument, TProjection> findFluent,
            int pageSize,
            int pageIndex)
        {
            return findFluent
                .Skip(pageSize * (pageIndex - 1))
                .Limit(pageSize);
        }
    }
}
