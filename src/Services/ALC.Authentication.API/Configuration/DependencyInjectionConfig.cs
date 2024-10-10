using ALC.Authentication.API.Services;
using EasyNetQ;

namespace ALC.Authentication.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection RegisterSerives(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddEasyNetQ("host=localhost:5672").UseSystemTextJson();

            return services;
        }
    }
}
