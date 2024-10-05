using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ALC.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = ALC.WebApp.MVC.Services.IAuthenticationService;

namespace ALC.Authentication.API.Controllers
{
    public class IdentityController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public IdentityController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("new-account")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            var response = await _authenticationService.Register(userRegister);
            await ExecuteLogin(response);
            return RedirectToAction("Index", "Catalog");
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
            await ExecuteLogin(response);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("exit")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Catalog");
        }    

        private async Task ExecuteLogin(UserResponse userResponse)
        {
            //get security Token from response
            JwtSecurityToken token = GetFormatedToken(userResponse.AccessToken);

            //get user claims
            var claims = new List<Claim>();
            //add token as claim
            claims.Add(new Claim("JWT", userResponse.AccessToken));
            //add other claims

            //set claims identity instance with claims and cookie auth type
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //set auth properties: expiration utc and persistence
            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTime.UtcNow.AddMinutes(60),
                IsPersistent = true
            };
            //sign in at httpContext with cookie set, auth schema and claims
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private static JwtSecurityToken GetFormatedToken(string jwtToken) 
            => new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
    }
}
