using Microsoft.AspNetCore.Diagnostics;
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
        
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCors("react-policy");
        app.UseStatusCodePages(HandleStatusCodePages);
        app.UseExceptionHandler(options => { });
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        return app;
    }

    private static async Task HandleStatusCodePages(StatusCodeContext context)
    {
        var requestPath = context.HttpContext.Request.Path;
        var traceId = context.HttpContext.TraceIdentifier;
        
        if (context.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden)
        {
            var details = CreateProblemDetails(
                "Forbidden",
                "You have not permission to access this API",
                StatusCodes.Status403Forbidden,
                requestPath,
                traceId
            );
            await context.HttpContext.Response.WriteAsJsonAsync(details);
        }

        if (context.HttpContext.Response.StatusCode == StatusCodes.Status401Unauthorized)
        {
            var details = CreateProblemDetails(
                "Unauthorized",
                "You are not authorized to access this API",
                StatusCodes.Status401Unauthorized,
                requestPath,
                traceId
            );
            await context.HttpContext.Response.WriteAsJsonAsync(details);
        }
    }

    private static ProblemDetails CreateProblemDetails(string title, string detail, int statusCode,
        string instance, string traceId)
    {
        var details = new ProblemDetails
        {
            Title = title,
            Detail = detail,
            Status = statusCode,
            Instance = instance
        };
        details.Extensions.Add("traceId", traceId);
        return  details;
    }
}