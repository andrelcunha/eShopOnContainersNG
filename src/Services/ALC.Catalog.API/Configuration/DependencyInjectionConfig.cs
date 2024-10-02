using ALC.Catalog.API.Data;
using ALC.Catalog.API.Repository;

namespace ALC.Catalog.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices (this IServiceCollection services)
    {    
        services.AddScoped<CatalogContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }
}