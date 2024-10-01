using ALC.Catalog.API.Models;
using ALC.Catalog.API.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ALC.Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _repository;
    public CatalogController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("/products")]
    public async Task<IEnumerable<Product>> GetAll()
    {
        return await _repository.GetProducts();
    }

    [HttpGet("/products/{id}")]
    public async Task<Product> Get(int id)
    {
        return await _repository.GetProduct(id);
    }
}

