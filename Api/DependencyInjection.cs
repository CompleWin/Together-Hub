using Api.Security.Extensions;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("react-policy", policy =>
            {
                policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:5173");
            });
        });
        
        services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(
                typeof(GetTopicsHandler).Assembly));

        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        
        services.AddOpenApi();
        services.AddControllers();
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddIdentityServices(configuration);
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors("react-policy");
        app.UseExceptionHandler(options => { });
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}