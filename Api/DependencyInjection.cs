using Api.Security.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

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
        services.AddControllers(options =>
        {
            var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            
            options.Filters.Add(new AuthorizeFilter(policy));
        });
        services.AddExceptionHandler<CustomExceptionHandler>();
        services.AddIdentityServices(configuration);
        
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors("react-policy");
        app.UseExceptionHandler(options => { });
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }
}