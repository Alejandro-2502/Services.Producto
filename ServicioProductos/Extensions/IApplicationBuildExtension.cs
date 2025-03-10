namespace Producto.Services.Extensions;

public static class IApplicationBuildExtension
{
    public static IApplicationBuilder Configure(this IApplicationBuilder app)
    {
        app.UseSwagger();
        UseSwaggerUI(app);
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }

    public static IApplicationBuilder UseSwaggerUI(this IApplicationBuilder app)
    {
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Servicio Producto");
        });

        return app;
    }
}
