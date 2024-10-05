using ALC.Authentication.API.Models;
using ALC.Authentication.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ALC.Authentication.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
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
            var user = new IdentityUser
            {
                UserName = userRegister.Email,
                Email = userRegister.Email,
                EmailConfirmed = userRegister.Email.Equals(userRegister.ConfirmPassword)
            };

            var result = await _userManager.CreateAsync(user, userRegister.Password);
        
            if(!result.Succeeded)
                return BadRequest(result.Errors);

            var token = await _tokenService.GenerateJwt(userRegister.Email);
            if (string.IsNullOrEmpty(token))
            {
                var errorResponse = new {message = "An error occurred while processing your request"};
                return StatusCode(500, errorResponse);
            }
            return Ok(token);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var result = await _signInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, false);

            if (result.Succeeded)
            {
                var token = await _tokenService.GenerateJwt(userLogin.Email);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }
}
