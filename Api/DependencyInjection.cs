namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenApi();
        services.AddControllers();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        
        return app;
    }
}