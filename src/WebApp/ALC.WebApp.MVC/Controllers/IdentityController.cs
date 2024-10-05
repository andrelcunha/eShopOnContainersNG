using ALC.WebApp.MVC.Models;
using ALC.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace ALC.Authentication.API.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public IdentityController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        // GET: Authentication
        [HttpGet]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UserLogin user)
        {
            var response = await _authenticationService.Login(user);
            
            return View();
        }

    }
}
