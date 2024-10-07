using ALC.Clients.API.Data;

namespace ALC.Clients.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ClientContext>();
    }
}
