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
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/error/500");
            app.UseStatusCodePagesWithRedirects("/error/{0}");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfig();

        app.UseLocaleConfig();

        app.UseMiddleware<ExceptionMiddleware>();

        app.UseEndpoints(c =>
            c.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
        );
    }
}
