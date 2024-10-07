
using ALC.WebApp.MVC.Extensions;
using ALC.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace ALC.WebApp.MVC.Services;

public class CatalogService : Service, ICatalogService
{
    private readonly HttpClient _httpClient;
    public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings){
        httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl??"");
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<ProductViewModel>> GetAll()
    {
        var response = _httpClient.GetAsync("products").Result;

        return await DeserializeResponseObject<IEnumerable<ProductViewModel>>(response);
    }

    public Task<ProductViewModel> GetById(Guid id)
    {
        var response = _httpClient.GetAsync($"products/{id}").Result;

        return DeserializeResponseObject<ProductViewModel>(response);
    }
}
