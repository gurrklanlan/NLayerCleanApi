using CleanApp.Api.ExceptionHandler;

namespace CleanApp.Api.Extension
{
    public static class ExcepitionHandlerExtension
    {
        public static IServiceCollection AddExceptionHandler(this IServiceCollection services)
        {
         services.AddExceptionHandler<CriticalExceptionHandler>();
           services.AddExceptionHandler<GlobalExceptionHandler>();

            return services;
        }
    }
}
