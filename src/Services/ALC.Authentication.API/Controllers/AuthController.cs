using ALC.Authentication.API.Models;
using ALC.Authentication.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALC.Authentication.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegister userRegister)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = userRegister.Email.Equals(userRegister.ConfirmPassword)
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);

            if (result.Succeeded)
            {
                var token = await _tokenService.GenerateJwt(userRegister.Email);
                if (token is null)
                {
                    var errorResponse = new { message = "An error occurred while processing your request" };
                    return StatusCode(500, errorResponse);
                }
                return Ok(token);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, true);

            if (result.Succeeded)
            {
                var token = await _tokenService.GenerateJwt(userLogin.Email);
                return Ok( token );
            }
            return Unauthorized();
        }
    }
}
