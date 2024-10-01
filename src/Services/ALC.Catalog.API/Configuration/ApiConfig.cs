using ALC.Catalog.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ALC.Catalog.API.Configuration;

public static class ApiConfig
{
    public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();

        services.AddCors(options =>
            options.AddPolicy("Total", 
                builder => 
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

        services.AddDependencyInjectionConfiguration();
    }

    public static void UseApiConfiguration(this IApplicationBuilder app, IHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwaggerConfiguration();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints( endpoint => 
        {
            endpoint.MapControllers();
        });
    }
}
