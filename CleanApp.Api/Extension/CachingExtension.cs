using App.Application.Contracts.Caching;

namespace CleanApp.Api.Extension
{
    public static class CachingExtension
    {
        public static IServiceCollection AddCaching(this IServiceCollection services
            )
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, ICacheService>();
            return services;
        }
    }
}
