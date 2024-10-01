
using ALC.Catalog.API.Configuration;

namespace ALC.Catalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddApiConfiguration(configuration);

        builder.Services.AddSwaggerConfiguration();

        var app = builder.Build();
        var env =  app.Environment;

        app.UseMigration();

        app.UseApiConfiguration(env);    
  
        app.Run();
    }
}
