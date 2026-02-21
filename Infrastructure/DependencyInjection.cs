using Application.Security.Services;
using Infrastructure.Security.Extensions;
using Infrastructure.Security.Services;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration
            .GetConnectionString("SqliteConnection")!;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        services.AddIdentityServices(configuration);
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
        
        return services;
    }
}