namespace CleanApp.Api.Extension
{
    public static class ConfigutePipLineExtension
    {
        public static IApplicationBuilder UseConfigurePipeLine(this WebApplication app)
        {
            

            app.UseExceptionHandler(x => { });

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerExt();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();
            return app;
        }
    }
}
