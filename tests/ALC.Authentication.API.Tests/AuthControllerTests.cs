using ALC.Authentication.API.Authentication;
using ALC.Authentication.API.Controllers;
using ALC.Authentication.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ALC.Authentication.API.Tests;

public class AuthControllerTests
{
    private readonly AuthController _controller;
    private readonly IOptions<JwtSettings> _options;

    public AuthControllerTests()
    {
        var jwtSettings = new JwtSettings
        {
             Key = "emblem-dullness-hastiness-epiphany-turkey",
            Issuer = "YourIssuer",
            Audience = "YourAudience",
            ExpiryHours = 1
        };

        _options = Options.Create(jwtSettings);
        _controller = new AuthController(_options);

    }
    [Fact]
    public void Login_ReturnsOk_WithValidUser()
    {
        var loginModel = new UserLoginModel {Email = "test", Password = "password"};
        var result = _controller.Login(loginModel);
        Console.WriteLine(result);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void Login_ReturnsUnatorized_WithIvalidUser ()
    {
        var loginModel = new UserLoginModel {Email = "invalid", Password = "invalid"};
        var result = _controller.Login(loginModel);
        Assert.IsType<UnauthorizedResult>(result);
    }

}