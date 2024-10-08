using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ALC.Authentication.API.Models;
using ALC.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ALC.Authentication.API.Services
{
    public class TokenService:ITokenService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _settings;


        public TokenService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> settings)
        {
            _userManager = userManager;
            _settings = settings.Value;
        }

        public async Task<UserResponse> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                var claims = await _userManager.GetClaimsAsync(user);            
                var identityClaims = await GetUserClaims(claims, user);
                var token =  EncodeJwt(identityClaims);
                return GenerateUserResponse(token, user, claims);
            }
            return null;
        }

        private async Task<ClaimsIdentity> GetUserClaims(ICollection<Claim> claims, IdentityUser user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);
        
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty));
            claims.Add(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles){
                claims.Add(new Claim("role", userRole));
            }
        
            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);

            return identityClaims;
        }

        private string EncodeJwt(ClaimsIdentity identityClaims)
        {
            var tokenHandler =  new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_settings.Key);
 
            var credencials = new SigningCredentials(
                new SymmetricSecurityKey(key), 
                SecurityAlgorithms.HmacSha256Signature);

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

        private UserResponse GenerateUserResponse(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
        {
            return new UserResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_settings.ExpiryHours).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email ?? string.Empty,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };
        }

        private static long ToUnixEpochDate(DateTime date) 
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

    }
}
