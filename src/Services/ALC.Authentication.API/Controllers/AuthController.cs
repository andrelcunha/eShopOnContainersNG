using ALC.Authentication.API.Models;
using ALC.Authentication.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ALC.WebAPI.Controllers;
using ALC.Core.Messages.Integration;
using EasyNetQ;

namespace ALC.Authentication.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        private IBus _bus;

        public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ITokenService tokenService, IBus bus)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
            _bus = bus;
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
                //Something here => integration
                var success = await RegisterCustomer(userRegister);

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

        private async Task<ResponseMessage> RegisterCustomer(UserRegister userRegister)
        {
            var user =  await _userManager.FindByEmailAsync(userRegister.Email);
            var registeredUser = new UserRegisteredIntegrationEvent(
                Guid.Parse(user.Id), 
                userRegister.Name, 
                userRegister.Email,
                userRegister.Cpf);

            // _bus = RabbitHutch.CreateBus("host=localhost:5672");
            var success = await _bus.Rpc.RequestAsync<UserRegisteredIntegrationEvent, ResponseMessage> (registeredUser);
            return success;

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
