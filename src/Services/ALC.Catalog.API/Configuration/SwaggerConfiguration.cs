namespace ALC.Catalog.API.Configuration;

public static class SwaggerConfiguration
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    
    public static void UseSwaggerConfiguration (this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

}
