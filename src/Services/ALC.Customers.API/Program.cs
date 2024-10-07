
using ALC.Customers.API.Configuration;
using ALC.WebAPI.Core.Identidade;

namespace ALC.Customers.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        builder.Services.AddApiConfiguration(configuration);

        builder.Services.AddJwtConfiguration(configuration);

        builder.Services.AddSwaggerConfiguration();

        builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssemblyContaining<Program>());

        builder.Services.RegisterServices();

        var app = builder.Build();
        var env = app.Environment;

        app.UseApiConfiguration(env);

        app.Run();
    }
}
