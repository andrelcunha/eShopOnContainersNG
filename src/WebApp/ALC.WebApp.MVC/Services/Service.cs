using System;
using System.Text.Json;

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

    protected static T? DeserializeResponseObject<T>(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<T>(json, options);
    }

}
