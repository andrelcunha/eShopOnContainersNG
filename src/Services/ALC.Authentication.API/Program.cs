using ALC.Authentication.API.Configuration;
using ALC.Authentication.API.Services;

namespace ALC.Authentication.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddIdentityConfiguration(configuration);

        builder.Services.AddApiConfiguration();

        builder.Services.AddSwaggerConfiguration();

        builder.Services.AddScoped<ITokenService, TokenService>();

        var app = builder.Build();
        var env =  app.Environment;

        app.UseMigration();

        app.UseApiConfiguration(env);

        app.Run();
    }
}
