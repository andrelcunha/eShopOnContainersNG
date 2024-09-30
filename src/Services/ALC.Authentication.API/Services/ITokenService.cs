namespace ALC.Authentication.API.Services;

public interface ITokenService
{
    Task<string> GenerateJwt(string email);
}