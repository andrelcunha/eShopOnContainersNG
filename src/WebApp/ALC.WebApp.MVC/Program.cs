using ALC.WebApp.MVC.Configuration;

namespace ALC.WebApp.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var configuration = builder.Configuration;

        builder.Services.AddIdentityConfig();

        builder.Services.AddMvcConfig(configuration);

        builder.Services.RegisterServices();

        var app = builder.Build();
        var env = builder.Environment;

        app.UseMvcConfig(env);
        
        app.Run();
    }
}
