using Infastructure.Data.DataBaseContext;

namespace Api.Security.Extensions;

public static class IdentityOptionsExtensions
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddIdentityCore<CustomIdentityUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication();
        services.AddScoped<IJwtSecurityService, JwtSecurityService>();
        
        return services;
    }
}