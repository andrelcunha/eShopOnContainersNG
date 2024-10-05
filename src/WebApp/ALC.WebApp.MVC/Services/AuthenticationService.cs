using System.Text.Json;
using ALC.WebApp.MVC.Extensions;
using ALC.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ALC.WebApp.MVC.Services;

public class AuthenticationService : Service, IAuthenticationService
{
    private readonly HttpClient _httpClient;
    
    public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl??"");
        _httpClient = httpClient;
    }

    public async Task<UserResponse> Login(UserLogin userLogin)
    {
        var content = GetContent(userLogin);

        var response = await _httpClient.PostAsync("login", content);

   
        return await DeserializeResponseObject<UserResponse>(response);
    }

    public async Task<UserResponse> Register(UserRegister userRegister)
    {
        var content = GetContent(userRegister);

        var response = await _httpClient.PostAsync("register", content);

        return await DeserializeResponseObject<UserResponse>(response);
    }
}
