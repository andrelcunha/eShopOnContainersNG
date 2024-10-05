using ALC.Authentication.API.Models;

namespace ALC.Authentication.API.Services
{
    public interface ITokenService
    {
        Task<UserResponse> GenerateJwt(string email);
    }
}