using ALC.Customers.API.Data;

namespace ALC.Customers.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<CustomerContext>();
    }
}
