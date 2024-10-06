using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ALC.WebApp.MVC.Models;

public class UserLogin
{
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The field {0} is in a invalid format")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}

public class UserRegister
{
    [Required(ErrorMessage = "The field {0} is required")]
    [EmailAddress(ErrorMessage = "The field {0} is in a invalid format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;

    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [DisplayName("Confirm your password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class UserResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; } = new UserToken();
    public ResponseResult ResponseResult { get; set; } = new ResponseResult();
}
public class UserToken
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public IEnumerable<UserClaim> Claims { get; set; } = new List<UserClaim>();
}

public class UserClaim
{
    public string Value { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
