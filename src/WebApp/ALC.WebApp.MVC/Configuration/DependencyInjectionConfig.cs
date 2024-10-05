using System;
using ALC.WebApp.MVC.Extensions;
using ALC.WebApp.MVC.Services;
using ALC.WebApp.MVC.Services.Handlers;

namespace ALC.WebApp.MVC.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpClient<IAuthenticationService, AuthenticationService>();
        services.AddHttpClient<ICatalogService, CatalogService>()
            .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUser,AspNetUser>();

    }
}
