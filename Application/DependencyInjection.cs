using Application.Topics;
using Application.Topics.Obselete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ITopicService, TopicService>();
        return services;
    }
    
}