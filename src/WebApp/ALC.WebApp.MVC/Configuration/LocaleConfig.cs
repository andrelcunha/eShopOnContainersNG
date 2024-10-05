using System.Globalization;
using Microsoft.AspNetCore.Localization;

namespace ALC.WebApp.MVC.Configuration;

public static class LocaleConfig
{
    public static void UseLocaleConfig(this IApplicationBuilder app)
    {
        var supportedCultures = new[] { new CultureInfo("pt-BR") };
        app.UseRequestLocalization(new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture("pt-BR"),
            SupportedCultures = supportedCultures,
            SupportedUICultures = supportedCultures
        });
    }
}
