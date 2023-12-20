using OnlineStore.CatalogService.Domain.Entities;

namespace OnlineStore.CatalogService.Infrastructure.DataAccess
{
    public static class DataForSeeding
    {
        public static List<ApplicationType> GetApplicationTypes()
        {
            return new List<ApplicationType>
            {
                new ApplicationType
                {
                    Name = "BMW"
                },
                new ApplicationType
                {
                    Name = "Honda"
                }
            };
        }

        public static List<Category> GetCategories()
        {
            return new List<Category>
            {
               new Category
               {
                   Name = "Wheels"
               },
               new Category
               {
                   Name = "Oils"
               }
            };
        }
        public static List<Product> GetProducts()
        {
            return new List<Product>
            {
               new Product
               {
                   Name = "BBS",
                   Price = 129
               },
               new Product
               {
                    Name = "Synthetic oil",
                    Price = 11
               }
            };
        }
    }
}
