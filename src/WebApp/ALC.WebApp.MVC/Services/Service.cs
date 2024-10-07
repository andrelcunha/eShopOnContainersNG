using System;
using System.Text.Json;
using ALC.WebApp.MVC.Extensions;

namespace ALC.WebApp.MVC.Services;

public abstract class Service
{
    protected static StringContent GetContent(object data)
    {
        return new StringContent(
            JsonSerializer.Serialize(data),
            System.Text.Encoding.UTF8,
            "application/json"
        );
    }

    protected async Task<T> DeserializeResponseObject<T>(HttpResponseMessage response)
    {
        string json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }

    protected bool HandleResponseErrors(HttpResponseMessage response)
    {
        switch ((int)response.StatusCode)
        {
            case 401:
            case 403:
            case 404:
            case 500:
                throw new CustomHttpRequestException(response.StatusCode);

            case 400:
                return false;
        }

        response.EnsureSuccessStatusCode();
        return true;
    }

}
