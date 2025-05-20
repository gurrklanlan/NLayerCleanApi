namespace CleanApp.Api.Extension
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "CleanApp.Api", Version = "v1" });
            });


            return services;

        }

        public static IApplicationBuilder UseSwaggerExt (this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
