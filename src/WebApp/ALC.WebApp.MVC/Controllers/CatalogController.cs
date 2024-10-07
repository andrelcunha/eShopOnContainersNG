using ALC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ALC.WebApp.MVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        // GET: CatalogController
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index()
        {
            var products = await _catalogService.GetAll();
            return View(products);
        }

        [HttpGet]
        [Route("product-details/{id}")]
        public async Task<ActionResult> ProductDetails(Guid id)
        {
            var product = await _catalogService.GetById(id);
            return View(product);
        }

    }
}
