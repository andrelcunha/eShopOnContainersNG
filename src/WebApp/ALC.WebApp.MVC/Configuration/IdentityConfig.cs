using System;

namespace ALC.WebApp.MVC.Configuration;

public static class IdentityConfig
{
    public static void AddIdentityConfig(this IServiceCollection services)
    {

    }

    public static void UseIdentityConfig(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
