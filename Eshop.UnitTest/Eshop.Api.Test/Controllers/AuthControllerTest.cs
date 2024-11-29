using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Api.Controllers;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Response.Auth;
using Eshop.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Eshop.Api.Test.Controllers
{
  public class AuthControllerTest
  {
    private Mock<IAuthService> _authService;
    private AuthController _authController;
    public AuthControllerTest()
    {
      _authService = new Mock<IAuthService>();
      _authController = new AuthController(_authService.Object);
    }
    //register
    [Fact]
    public async void Register_ReturnsOKResult_WithRegisterResponse()
    {
      //arrange
      var registerRequest = new RegisterDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        FirstName = "Xxxx",
        LastName = "Xxxx",
        Username = "Xxxx",
        Password = "Xxxxx"
      };
      var registerResponse = new RegisterDtoResponse
      {
        Email = "Xxxx@hotmail.com",
        FirstName = "Xxxx",
        LastName = "Xxxx",
        Username = "Xxxx"
      };

      _authService.Setup(t => t.Register(registerRequest)).ReturnsAsync(registerResponse);

      //act
      var response = await _authController.Register(registerRequest);

      //assert
      var okResult = Assert.IsType<OkObjectResult>(response.Result);
      var Value = Assert.IsType<RegisterDtoResponse>(okResult.Value);
      Assert.NotNull(Value);
      Assert.Equal("Xxxx", Value.Username);
      Assert.Equal("Xxxx", Value.FirstName);
      Assert.Equal("Xxxx", Value.LastName);
      Assert.Equal("Xxxx@hotmail.com", Value.Email);
    }
    [Fact]
    public async void Register_shouldReturnEmailExist_ReturnsBadRequestResult()
    {
      //arrange
      var registerRequest = new RegisterDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        FirstName = "Xxxx",
        LastName = "Xxxx",
        Username = "Xxxx",
        Password = "Xxxxx"
      };

      _authService.Setup(t => t.Register(registerRequest)).ThrowsAsync(new Exception("email already exist"));

      //act
      var response = await _authController.Register(registerRequest);

      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      Assert.Equal("email already exist", BadRequestResult.Value);
    }
    [Fact]
    public async void Register_shouldReturnUserNameExist_ReturnsBadRequestResult()
    {
      //arrange
      var registerRequest = new RegisterDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        FirstName = "Xxxx",
        LastName = "Xxxx",
        Username = "Xxxx",
        Password = "Xxxxx"
      };

      _authService.Setup(t => t.Register(registerRequest)).ThrowsAsync(new Exception("UserName already taken"));

      //act
      var response = await _authController.Register(registerRequest);

      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      Assert.Equal("UserName already taken", BadRequestResult.Value);
    }
    [Fact]
    public async void Register_WithAllMissingField_ReturnsBadRequestResult()
    {
      //arrange
      var registerRequest = new RegisterDtoRequest { };
      var registerResponse = new RegisterDtoResponse { };
      _authService.Setup(t => t.Register(registerRequest)).ReturnsAsync(registerResponse);
      //act
      var response = await _authController.Register(registerRequest);
      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(BadRequestResult.Value);
      Assert.Equal("First name is required.", value.FirstOrDefault(x => x.PropertyName == "FirstName").ErrorMessage);
      Assert.Equal("Last name is required.", value.FirstOrDefault(x => x.PropertyName == "LastName").ErrorMessage);
      Assert.Equal("Username is required.", value.FirstOrDefault(x => x.PropertyName == "Username").ErrorMessage);
      Assert.Equal("Email is required.", value.FirstOrDefault(x => x.PropertyName == "Email").ErrorMessage);
      Assert.Equal("Password is required.", value.FirstOrDefault(x => x.PropertyName == "Password").ErrorMessage);

    }
    [Fact]
    public async void Register_InvalidLength_ReturnsBadRequestResult()
    {
      //arrange
      var registerRequest = new RegisterDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        FirstName = "Xx",
        LastName = "Xx",
        Username = "Xx",
        Password = "Xxx"
      };
      var registerResponse = new RegisterDtoResponse { };
      _authService.Setup(t => t.Register(registerRequest)).ReturnsAsync(registerResponse);
      //act
      var response = await _authController.Register(registerRequest);
      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(BadRequestResult.Value);
      Assert.Equal("character length of firstname must be between 3 and 20", value.FirstOrDefault(x => x.PropertyName == "FirstName").ErrorMessage);
      Assert.Equal("character length of lastname must be between 3 and 20", value.FirstOrDefault(x => x.PropertyName == "LastName").ErrorMessage);
      Assert.Equal("character length of username must be between 3 and 20", value.FirstOrDefault(x => x.PropertyName == "Username").ErrorMessage);
      Assert.Equal("Password must be at least 5 characters long.", value.FirstOrDefault(x => x.PropertyName == "Password").ErrorMessage);
    }
    [Fact]
    public async void Register_InvalidRegexOrInvalidEmail_ReturnsBadRequestResult()
    {
      //arrange
      var registerRequest = new RegisterDtoRequest
      {
        Email = "Xxxx",
        FirstName = "xxxx12",
        LastName = "xxxx12",
        Username = "xxxx ",
        Password = "Xxxxx"
      };
      var registerResponse = new RegisterDtoResponse { };
      _authService.Setup(t => t.Register(registerRequest)).ReturnsAsync(registerResponse);
      //act
      var response = await _authController.Register(registerRequest);
      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(BadRequestResult.Value);
      Assert.Equal("first letter should be uppercase and must not contain space and number", value.FirstOrDefault(x => x.PropertyName == "FirstName").ErrorMessage);
      Assert.Equal("first letter should be uppercase and must not contain space and number", value.FirstOrDefault(x => x.PropertyName == "LastName").ErrorMessage);
      Assert.Equal("first letter should be uppercase and must not contain space", value.FirstOrDefault(x => x.PropertyName == "Username").ErrorMessage);
      Assert.Equal("Invalid email address.", value.FirstOrDefault(x => x.PropertyName == "Email").ErrorMessage);
    }
    //login
    [Fact]
    public async void Login_ReturnsOKResult_WithLoginResponse()
    {
      //arrange
      var LoginRequest = new LoginDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        Password = "Xxxxx"
      };
      var loginResponse = new LoginDtoResponse
      {
        Message = "Success",
        AccessToken = "t3yaIv2wliglfTFIwHJh1xL4qgX7ZE4PRLt5lhd3XKp452GpsUP84p0ooh2f5T9m4sp96q2Eo8YZtb3wSjze07u9EiFvPlKBv3CR8",
        refreshToken = "tq5LXur7lk5geR4dC4IHa9Sf8avKhhlZCO1YWqgA3cVz4IP2JxbgoQtTUKk3PHPPj0wmHT6vOp0mibFxqtLUEyBdPjaZ6l6OaMOYb",
        Email = "Xxxx@hotmail.com",
      };
      _authService.Setup(t => t.Login(LoginRequest)).ReturnsAsync(loginResponse);
      //act
      var response = await _authController.Login(LoginRequest);
      //assert
      var okResult = Assert.IsType<OkObjectResult>(response.Result);
      var Value = Assert.IsType<LoginDtoResponse>(okResult.Value);
      Assert.NotNull(Value);
      Assert.Equal("Xxxx@hotmail.com", Value.Email);
      Assert.Equal("Success", Value.Message);
      Assert.Equal("t3yaIv2wliglfTFIwHJh1xL4qgX7ZE4PRLt5lhd3XKp452GpsUP84p0ooh2f5T9m4sp96q2Eo8YZtb3wSjze07u9EiFvPlKBv3CR8", Value.AccessToken);
    }
    //login validation error
    [Fact]
    public async void Login_WithAllMissingField_ReturnsBadRequestResult()
    {
      //arrange
      var LoginRequest = new LoginDtoRequest { };
      var loginResponse = new LoginDtoResponse { };
      _authService.Setup(t => t.Login(LoginRequest)).ReturnsAsync(loginResponse);
      //act
      var response = await _authController.Login(LoginRequest);
      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(BadRequestResult.Value);
      Assert.Equal("Email is required.", value.FirstOrDefault(x => x.PropertyName == "Email").ErrorMessage);
      Assert.Equal("Password is required.", value.FirstOrDefault(x => x.PropertyName == "Password").ErrorMessage);

    }
    [Fact]
    public async void Login_WithInvalidEmailAndPasswordLength_ReturnsBadRequestResult()
    {
      //arrange
      var LoginRequest = new LoginDtoRequest
      {
        Email = "xdsdsd",
        Password = "xd",
      };
      var loginResponse = new LoginDtoResponse { };
      _authService.Setup(t => t.Login(LoginRequest)).ReturnsAsync(loginResponse);
      //act
      var response = await _authController.Login(LoginRequest);
      //assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, BadRequestResult.StatusCode);
      var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(BadRequestResult.Value);
      Assert.Equal("Invalid email address.", value.FirstOrDefault(x => x.PropertyName == "Email").ErrorMessage);
      Assert.Equal("Password must be at least 5 characters long.", value.FirstOrDefault(x => x.PropertyName == "Password").ErrorMessage);
    }
    //login exception
    [Fact]
    public async void Login_shouldReturnUserWithThisEmailNotExist_ReturnsNotFoundRequestResult()
    {
      //arrange
      var loginRequest = new LoginDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        Password = "Xxxxx"
      };
      _authService.Setup(t => t.Login(loginRequest)).ThrowsAsync(new KeyNotFoundException("user doesn't exist"));
      //act
      var response = await _authController.Login(loginRequest);

      //assert
      var NotFoundRequestResult = Assert.IsType<NotFoundObjectResult>(response.Result);
      Assert.Equal(404, NotFoundRequestResult.StatusCode);
      Assert.Equal("user doesn't exist", NotFoundRequestResult.Value);
    }
    [Fact]
    public async void Login_shouldReturnWrongPassword_ReturnsUnauthorizedRequestResult()
    {
      //arrange
      var loginRequest = new LoginDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        Password = "Xxxxx"
      };
      _authService.Setup(t => t.Login(loginRequest)).ThrowsAsync(new UnauthorizedAccessException("wrong password"));
      //act
      var response = await _authController.Login(loginRequest);

      //assert
      var UnauthorizedRequestResult = Assert.IsType<UnauthorizedObjectResult>(response.Result);
      Assert.Equal(401, UnauthorizedRequestResult.StatusCode);
      Assert.Equal("wrong password", UnauthorizedRequestResult.Value);
    }

    //refreshtoken
    [Fact]
    public async void RefreshToken_ReturnsOKResult_WithLoginResponse()
    {
      //arrange
      var RefreshTokenRequest = new RefreshTokenDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        RefreshToken = "tq5LXur7lk5geR4dC4IHa9Sf8avKhhlZCO1YWqgA3cVz4IP2JxbgoQtTUKk3PHPPj0wmHT6vOp0mibFxqtLUEyBdPjaZ6l6OaMOYb"
      };
      var loginResponse = new LoginDtoResponse
      {
        Message = "Success",
        AccessToken = "t3yaIv2wliglfTFIwHJh1xL4qgX7ZE4PRLt5lhd3XKp452GpsUP84p0ooh2f5T9m4sp96q2Eo8YZtb3wSjze07u9EiFvPlKBv3CR8",
        refreshToken = "tq5LXur7lk5geR4dC4IHa9Sf8avKhhlZCO1YWqgA3cVz4IP2JxbgoQtTUKk3PHPPj0wmHT6vOp0mibFxqtLUEyBdPjaZ6l6OaMOYb",
        Email = "Xxxx@hotmail.com",
      };
      _authService.Setup(t => t.RefreshToken(RefreshTokenRequest)).ReturnsAsync(loginResponse);
      //act
      var response = await _authController.RefreshToken(RefreshTokenRequest);
      //assert
      var okResult = Assert.IsType<OkObjectResult>(response.Result);
      var Value = Assert.IsType<LoginDtoResponse>(okResult.Value);
      Assert.NotNull(Value);
      Assert.Equal("Xxxx@hotmail.com", Value.Email);
      Assert.Equal("Success", Value.Message);
      Assert.Equal("t3yaIv2wliglfTFIwHJh1xL4qgX7ZE4PRLt5lhd3XKp452GpsUP84p0ooh2f5T9m4sp96q2Eo8YZtb3wSjze07u9EiFvPlKBv3CR8", Value.AccessToken);
    }
    //refreshtoken exception
    [Fact]
    public async void RefreshToken_shouldReturnInvalidRefreshToken_ReturnsUnauthorizedRequestResult()
    {
      //arrange
      var RefreshTokenRequest = new RefreshTokenDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        RefreshToken = "tq5LXur7lk5geR4dC4IHa9Sf8avKhhlZCO1YWqgA3cVz4IP2JxbgoQtTUKk3PHPPj0wmHT6vOp0mibFxqtLUEyBdPjaZ6l6OaMOYb"
      };
      _authService.Setup(t => t.RefreshToken(RefreshTokenRequest)).ThrowsAsync(new UnauthorizedAccessException("Invalid or expired refresh token"));
      //act
      var response = await _authController.RefreshToken(RefreshTokenRequest);

      //assert
      var UnauthorizedRequestResult = Assert.IsType<UnauthorizedObjectResult>(response.Result);
      Assert.Equal(401, UnauthorizedRequestResult.StatusCode);
      Assert.Equal("Invalid or expired refresh token", UnauthorizedRequestResult.Value);
    }

    [Fact]
    public async void RefreshToken_shouldReturnUSerNotFound_ReturnsNotFoundResult()
    {
      //arrange
      var RefreshTokenRequest = new RefreshTokenDtoRequest
      {
        Email = "Xxxx@hotmail.com",
        RefreshToken = "tq5LXur7lk5geR4dC4IHa9Sf8avKhhlZCO1YWqgA3cVz4IP2JxbgoQtTUKk3PHPPj0wmHT6vOp0mibFxqtLUEyBdPjaZ6l6OaMOYb"
      };
      _authService.Setup(t => t.RefreshToken(RefreshTokenRequest)).ThrowsAsync(new KeyNotFoundException("user doesn't exist"));
      //act
      var response = await _authController.RefreshToken(RefreshTokenRequest);

      //assert
      var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
      Assert.Equal(404, NotFoundResult.StatusCode);
      Assert.Equal("user doesn't exist", NotFoundResult.Value);
    }
    //refreshtoken Validation error
    [Fact]
    public async void RefreshToken_ShouldReturnRequiredError_ReturnsBadRequestResult()
    {
      // arrange
      var RefreshTokenRequest = new RefreshTokenDtoRequest { };
      var loginResponse = new LoginDtoResponse { };
      _authService.Setup(t => t.RefreshToken(RefreshTokenRequest)).ReturnsAsync(loginResponse);
      _authController.ModelState.AddModelError("Email", "Email is required");
      _authController.ModelState.AddModelError("RefreshToken", "refreshToken is required");
      // act
      var response = await _authController.RefreshToken(RefreshTokenRequest);
      // assert
      var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, badrequestResult.StatusCode);
      var value = Assert.IsType<SerializableError>(badrequestResult.Value);
      Assert.Contains("Email is required", value["Email"] as string[]);
      Assert.Contains("refreshToken is required", value["RefreshToken"] as string[]);
    }
    [Fact]
    public async void RefreshToken_ShouldReturnInvalidEmailAndRefreshTokenlengthError_ReturnsBadRequestResult()
    {
      // arrange
      var RefreshTokenRequest = new RefreshTokenDtoRequest
      {
        Email = "admin",
        RefreshToken = "dsdf"
      };
      var loginResponse = new LoginDtoResponse { };
      _authService.Setup(t => t.RefreshToken(RefreshTokenRequest)).ReturnsAsync(loginResponse);
      _authController.ModelState.AddModelError("Email", "should be valid email adress");
      _authController.ModelState.AddModelError("RefreshToken", "min length should be 50 character");
      // act
      var response = await _authController.RefreshToken(RefreshTokenRequest);
      //assert
      var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400, badrequestResult.StatusCode);
      var value = Assert.IsType<SerializableError>(badrequestResult.Value);
      Assert.Contains("should be valid email adress", value["Email"] as string[]);
      Assert.Contains("min length should be 50 character", value["RefreshToken"] as string[]);
    }

    //make-Admin
    [Fact]
    public async void MakeAdmin_Valid_ReturnOKResult()
    {
      // arrange
      var email = "xxxx@live.be";
      var message = "user is now admin";
      _authService.Setup(t => t.MakeAdmin(email)).ReturnsAsync(message);
      // act
      var response = await _authController.MakeAdmin(email);
      // assert
      var okResult = Assert.IsType<OkObjectResult>(response.Result);
      var Value = Assert.IsType<string>(okResult.Value);
      Assert.Equal("user is now admin",Value);
    }
    [Fact]
    public async void MakeAdmin_shouldReturnEmailisNotFoundException_ReturnNotFoundResult()
    {
      // arrange
      var email = "xxxx@live.be";
      _authService.Setup(t => t.MakeAdmin(email)).ThrowsAsync(new KeyNotFoundException("invalid username or username not found"));
      // act
      var response = await _authController.MakeAdmin(email);
      // assert
      var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
      Assert.Equal(404,NotFoundResult.StatusCode);
      var Value = Assert.IsType<string>(NotFoundResult.Value);
      Assert.Equal("invalid username or username not found",Value);
    }
    [Fact]
    public async void MakeAdmin_shouldReturnUserAlreadyAdmin_ReturnBadRequestResult()
    {
      // arrange
      var email = "xxxx@live.be";
      _authService.Setup(t => t.MakeAdmin(email)).ThrowsAsync(new Exception("user is already admin"));
      // act
      var response = await _authController.MakeAdmin(email);
      // assert
      var BadRequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
      Assert.Equal(400,BadRequestResult.StatusCode);
      var Value = Assert.IsType<string>(BadRequestResult.Value);
      Assert.Equal("user is already admin",Value);
    }
  }
}