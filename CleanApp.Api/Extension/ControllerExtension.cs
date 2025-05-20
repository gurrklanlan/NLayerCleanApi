using CleanApp.Api.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CleanApp.Api.Extension
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddControllerWithFilters(this IServiceCollection services)
        {
            services.AddControllers(options =>
           {
               options.Filters.Add<FluentValidationFilter>();
               options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
           });
            return services;
        }
    }
}
