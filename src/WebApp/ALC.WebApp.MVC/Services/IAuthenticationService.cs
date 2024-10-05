using System;
using ALC.WebApp.MVC.Models;

namespace ALC.WebApp.MVC.Services;

public interface IAuthenticationService
{
    Task<UserResponse> Login(UserLogin userLogin);
    Task<UserResponse> Register(UserRegister userRegister);
}
