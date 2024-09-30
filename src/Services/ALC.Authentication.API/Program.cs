
using ALC.Authentication.API.Configuration;

namespace ALC.Authentication.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
 
        builder.Services.AddApiConfiguration();

        builder.Services.AddSwaggerConfiguration();

        var app = builder.Build();
        var env =  app.Environment;

        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerConfiguration();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
