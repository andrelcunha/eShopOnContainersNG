using System;
using ALC.WebApp.MVC.Services;

namespace ALC.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<ICatalogService, CatalogService>();
    }
}
