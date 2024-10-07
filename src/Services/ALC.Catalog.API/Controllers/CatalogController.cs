using ALC.Catalog.API.Models;
using ALC.Catalog.API.Repository;
using ALC.WebAPI.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ALC.WebAPI.Core.Identidade.RequiredClaimFilter;

namespace ALC.Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CatalogController : MainController
    {
        private readonly IProductRepository _repository;
        public CatalogController(IProductRepository repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet("products")]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _repository.GetProducts();
        }

        [ClaimsAuthorize("Catalog","Read")]
        [HttpGet("products/{id}")]
        public async Task<Product> Get(Guid id)
        {
            return await _repository.GetProduct(id);
        }
    }

}
