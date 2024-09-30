using ALC.Authentication.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ALC.Authentication.API.Configuration;

public static class MigrationConfig
{
    public static void UseMigration (this IApplicationBuilder app)
    {
        // Apply migrations at startup
        using var scope = app.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AuthDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }

} 