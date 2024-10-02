using System;
using ALC.Authentication.API.Services;

namespace ALC.Authentication.API.Configuration;

public static class DependencyInjectionConfig
{
    public static IServiceCollection RegisterSerives(this IServiceCollection services)
    {
        services.AddScoped<ITokenService, TokenService>();
        return services;
    }
}
