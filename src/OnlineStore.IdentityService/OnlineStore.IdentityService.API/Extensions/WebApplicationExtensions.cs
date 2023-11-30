using OnlineStore.IdentityService.DAL.Seeder.Contracts;

namespace OnlineStore.IdentityService.API.Extensions
{
    public static class WebApplicationExtensions
    {
        public static void InitializeDb(this WebApplication app)
        {
            app.MigrateDatabase();

            using (var scope = app.Services.CreateScope())
            {
                var dbSeeder =  scope.ServiceProvider.GetRequiredService<IDbSeeder>();
                dbSeeder!.Seed();
            }
        }
    }
}
