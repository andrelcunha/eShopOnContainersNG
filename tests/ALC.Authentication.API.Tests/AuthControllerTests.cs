using ALC.Authentication.API.Authentication;
using ALC.Authentication.API.Controllers;
using ALC.Authentication.API.Models;
using ALC.Authentication.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Moq;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ALC.Authentication.API.Tests
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly IOptions<JwtSettings> _options;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;

        private readonly Mock<ITokenService> _tokenServiceMock;

        public AuthControllerTests()
        {
            var jwtSettings = new JwtSettings
            {
                 Key = "emblem-dullness-hastiness-epiphany-turkey",
                Issuer = "YourIssuer",
                Audience = "YourAudience",
                ExpiryHours = 1
            };

            _options = Options.Create(jwtSettings);

            _userManagerMock = new Mock<UserManager<IdentityUser>>(
                    new Mock<IUserStore<IdentityUser>>().Object,
                    null, null, null, null, null, null, null, null);

            _signInManagerMock = new Mock<SignInManager<IdentityUser>>(
                    _userManagerMock.Object,
                    new Mock<IHttpContextAccessor>().Object,
                    new Mock<IUserClaimsPrincipalFactory<IdentityUser>>().Object,
                    null, null, null, null);

            _tokenServiceMock = new Mock<ITokenService>();

            _controller = new AuthController(_signInManagerMock.Object, _userManagerMock.Object, _tokenServiceMock.Object);

        }
        [Fact]
        public async void Login_ReturnsOk_WithValidUser()
        {
            //Arrange
            var loginModel = new UserLogin {Email = "test", Password = "password"};
            _signInManagerMock.Setup(s => s.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false))
                    .ReturnsAsync(SignInResult.Success);
            _userManagerMock.Setup(u => u.FindByEmailAsync(loginModel.Email))
                    .ReturnsAsync(new IdentityUser { Email = loginModel.Email });
            _tokenServiceMock.Setup(t => t.GenerateJwt(It.IsAny<string>()))
                    .ReturnsAsync("generatedToken");
            //Action
            var result = await _controller.Login(loginModel);
            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Login_ReturnsUnatorized_WithIvalidUser ()
        {
            var loginModel = new UserLogin {Email = "invalid", Password = "invalid"};
            _signInManagerMock.Setup(s => s.PasswordSignInAsync(loginModel.Email, loginModel.Password, false, false))
                .ReturnsAsync(SignInResult.Failed);

            var result = await _controller.Login(loginModel);
        
            Assert.IsType<UnauthorizedResult>(result);
        }

    }
}