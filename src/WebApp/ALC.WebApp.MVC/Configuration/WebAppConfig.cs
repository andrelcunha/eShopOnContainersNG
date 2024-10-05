using System;
using ALC.WebApp.MVC.Extensions;

namespace ALC.WebApp.MVC.Configuration;

public static class WebAppConfig
{
    public static void AddMvcConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.Configure<AppSettings>(configuration);
    }

    public static void UseMvcConfig(this IApplicationBuilder app, IWebHostEnvironment env)
    {
          if (!env.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfig();

        app.UseEndpoints(c =>
            c.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
        );
    }
}
