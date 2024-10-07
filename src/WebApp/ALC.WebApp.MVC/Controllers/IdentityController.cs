using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ALC.WebApp.MVC.Controllers;
using ALC.WebApp.MVC.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = ALC.WebApp.MVC.Services.IAuthenticationService;

namespace ALC.Authentication.API.Controllers
{
    public class IdentityController : MainController
    {
        private readonly IAuthenticationService _authenticationService;

        public IdentityController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        [Route("new-account")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("new-account")]
        public async Task<ActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid) return View(userRegister);

            var response = await _authenticationService.Register(userRegister);

            if(ResponseHasErrors(response.ResponseResult)) return View(userRegister);

            await ExecuteLogin(response);

            return RedirectToAction("Index", "Catalog");
        }

        // GET: Authentication
        [HttpGet]
        [Route("login")]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(UserLogin user, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid) return View(user);
            
            var response = await _authenticationService.Login(user);
            
            if(ResponseHasErrors(response.ResponseResult)) return View(user);

            await ExecuteLogin(response);
            
            if (string.IsNullOrEmpty(returnUrl)) 
            {
                if(Url.IsLocalUrl(returnUrl))
                {
                    return RedirectToAction(returnUrl);
                }
                else return RedirectToAction("Index", "Catalog");
            }

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
