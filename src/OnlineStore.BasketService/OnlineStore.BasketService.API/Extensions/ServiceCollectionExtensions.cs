using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OnlineStore.BasketService.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEndPointsRegistration(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            });
        }

        public static void AddHealthChecksRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddRedis(configuration["CacheSettings:ConnectionString"]!, "Redis Health", HealthStatus.Degraded);
        }
    }
}
