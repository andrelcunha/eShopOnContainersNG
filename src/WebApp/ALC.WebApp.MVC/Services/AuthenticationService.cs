using System;
using System.Text.Json;
using ALC.WebApp.MVC.Extensions;
using ALC.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ALC.WebApp.MVC.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HttpClient _httpClient;
    
    public AuthenticationService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.AuthenticationUrl??"");
        _httpClient = httpClient;
    }

    public async Task<UserResponse> Login(UserLogin userLogin)
    {   
        var content = new StringContent(
            JsonSerializer.Serialize(userLogin),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("login", content);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        string json = await response.Content.ReadAsStringAsync();

        Console.WriteLine(json);

        return JsonSerializer.Deserialize<UserResponse>(json, options);
    }

    public async Task<UserResponse> Register(UserRegister userRegister)
    {
        var content = new StringContent(
            JsonSerializer.Serialize(userRegister),
            System.Text.Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync("register", content);

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<UserResponse>(await response.Content.ReadAsStringAsync(), options);
    }
}
