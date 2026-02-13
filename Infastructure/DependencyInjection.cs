using Application.Data.DataBaseContext;
using Infastructure.Data.DataBaseContext;
using Microsoft.Extensions.Configuration;

namespace Infastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        string connectionString = configuration
            .GetConnectionString("SqliteConnection")!;

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        return services;
    }
}