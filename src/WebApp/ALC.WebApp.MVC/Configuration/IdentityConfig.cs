using System;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ALC.WebApp.MVC.Configuration;

public static class IdentityConfig
{
    public static void AddIdentityConfig(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/login";
            options.AccessDeniedPath = "/error/403";
        });
    }

    public static void UseIdentityConfig(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
