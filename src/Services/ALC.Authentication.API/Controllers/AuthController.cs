using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using ALC.Authentication.API.Authentication;
using ALC.Authentication.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ALC.Authentication.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtSettings _settings;
        public AuthController(IOptions<JwtSettings> settings)
        {
            _settings = settings.Value;
        }

        [HttpPost("Register")]
        public IActionResult Register(UserRegisterModel userRegister)
        {
            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login(UserLoginModel userLogin)
        {
            if (userLogin.Email == "test" && userLogin.Password == "password" )
            {
                var token = GenerateJwtToken(userLogin.Email);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private string GenerateJwtToken(string email)
        {
            var tokenHandler =  new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.Key);
            Console.WriteLine(_settings.Key);
            var credencials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature);
            var claims = GetUserClaims(1, email);

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            var tokenDescriptor = new SecurityTokenDescriptor 
            {
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_settings.ExpiryHours),
                SigningCredentials = credencials,

            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
          
            return tokenHandler.WriteToken(token);
        }

        private ICollection<Claim> GetUserClaims(int id, string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new(JwtRegisteredClaimNames.Email, email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()),
                new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64),
            };
            return claims;
        }

        private static long ToUnixEpochDate(DateTime date) 
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
