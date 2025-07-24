using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Response.Auth;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Identity;
using Eshop.Infrastructure.Interface;
using Eshop.Infrastructure.Mapper;
using Eshop.Infrastructure.Services;
using Eshop.Infrastructure.Test.Fixture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Eshop.Infrastructure.Test.Service
{
    public class AuthServiceTest
    {
        private readonly IMapper _mapper;

        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AuthService _authService;
        public AuthServiceTest()
        {
            _authServiceMock = new Mock<IAuthService>();
            var store = new Mock<IUserStore<ApplicationUser>>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperAuthProfile>());
            _tokenServiceMock = new Mock<ITokenService>();
            _mapper = config.CreateMapper();
            _basketRepository = new Mock<IBasketRepository>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _authService = new AuthService(_userManagerMock.Object, _mapper, _tokenServiceMock.Object, _basketRepository.Object);
        }

        //register
        [Fact]
        public async Task Register_ShouldReturnRegisterDtoResponse_WhenCredentialIsValid()
        {
            //arrange
            var registerDtoRequest = new RegisterDtoRequest
            {
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
                Password = "Emre1997",
                Username = "EmreOzz"
            };
            var registerDtoResponse = new RegisterDtoResponse
            {
                Username = "EmreOzz",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync(registerDtoRequest.Email)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(x => x.FindByNameAsync(registerDtoRequest.Username)).ReturnsAsync((ApplicationUser)null);
            _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), registerDtoRequest.Password)).ReturnsAsync(IdentityResult.Success);

            //act
            var result = await _authService.Register(registerDtoRequest);
            //assert
            Assert.NotNull(result);
            Assert.IsType<RegisterDtoResponse>(result);
            Assert.Equal(registerDtoResponse.Email, result.Email);
            Assert.Equal(registerDtoResponse.FirstName, result.FirstName);
            Assert.Equal(registerDtoResponse.LastName, result.LastName);
            Assert.Equal(registerDtoResponse.Username, result.Username);
        }
        [Fact]
        public async Task Register_ShouldThrowException_WhenEmailExist()
        {
            //arrange
            var registerDtoRequest = new RegisterDtoRequest
            {
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
                Password = "Emre1997",
                Username = "EmreOzz"
            };
            var user = new ApplicationUser
            {
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync(registerDtoRequest.Email)).ReturnsAsync(user);
            //act&assert
            var error = await Assert.ThrowsAsync<Exception>(() => _authService.Register(registerDtoRequest));
            Assert.Equal("email already exist", error.Message);
        }
        [Fact]
        public async Task Register_ShouldThrowException_WhenUsernameExist()
        {
            //arrange
            var registerDtoRequest = new RegisterDtoRequest
            {
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
                Password = "Emre1997",
                Username = "EmreOzz"
            };
            var user = new ApplicationUser
            {
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
            };
            _userManagerMock.Setup(x => x.FindByNameAsync(registerDtoRequest.Username)).ReturnsAsync(user);
            //act&assert
            var error = await Assert.ThrowsAsync<Exception>(() => _authService.Register(registerDtoRequest));
            Assert.Equal("UserName already taken", error.Message);
        }
        //login
        [Fact]
        public async Task Login_ShouldReturnLoginDtoResponseAndCreateBasketRepo_WhenCredentialIsValidAndBasketRepodoesntExist()
        {
            //arrange
            var loginDtoRequest = new LoginDtoRequest
            {
                Email = "emre@live.be",
                Password = "Emre1997",
            };
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
            };
            var loginDtoResponse = new LoginDtoResponse
            {
                AccessToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai",
                refreshToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai",
                Message = "Success",
            };
            
            _tokenServiceMock.Setup(x => x.GenerateToken(user)).ReturnsAsync(loginDtoResponse.AccessToken);
            _tokenServiceMock.Setup(x => x.GenerateRefreshToken()).Returns(loginDtoResponse.refreshToken);
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginDtoRequest.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDtoRequest.Password)).ReturnsAsync(true);
            _basketRepository.Setup(x=>x.GetOneAsync(userId)).ReturnsAsync((Basket) null);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            //act
            var result = await _authService.Login(loginDtoRequest);
            //assert
            Assert.NotNull(result);
            Assert.IsType<LoginDtoResponse>(result);
            Assert.Equal(loginDtoResponse.Message, result.Message);
            Assert.Equal(loginDtoResponse.refreshToken, result.refreshToken);
            Assert.Equal(loginDtoResponse.AccessToken, result.AccessToken);
            _basketRepository.Verify(x => x.CreateAsync(It.Is<Basket>(b=>b.UserId==userId)),Times.Once);
            
        }
        [Fact]
        public async Task Login_ShouldReturnLoginDtoResponseAndVerifyBasketRepoExist_WhenCredentialIsValid()
        {
            //arrange
            var loginDtoRequest = new LoginDtoRequest
            {
                Email = "emre@live.be",
                Password = "Emre1997",
            };
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var user = new ApplicationUser
            {
                Id = userId,
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
            };
            var loginDtoResponse = new LoginDtoResponse
            {
                AccessToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai",
                refreshToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai",
                Message = "Success",
            };
            var basket = new Basket 
                {   Id=1,
                    UserId = "bad206e0-3980-4746-893b-80afc748dfea", 
                    BasketProducts= BasketProductFixture.AllProductInBasket()
                };
            _tokenServiceMock.Setup(x => x.GenerateToken(user)).ReturnsAsync(loginDtoResponse.AccessToken);
            _tokenServiceMock.Setup(x => x.GenerateRefreshToken()).Returns(loginDtoResponse.refreshToken);
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginDtoRequest.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, loginDtoRequest.Password)).ReturnsAsync(true);
            _basketRepository.Setup(x=>x.GetOneAsync(userId)).ReturnsAsync(basket);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(IdentityResult.Success);

            //act
            var result = await _authService.Login(loginDtoRequest);
            //assert
            Assert.NotNull(result);
            Assert.IsType<LoginDtoResponse>(result);
            Assert.Equal(loginDtoResponse.Message, result.Message);
            Assert.Equal(loginDtoResponse.refreshToken, result.refreshToken);
            Assert.Equal(loginDtoResponse.AccessToken, result.AccessToken);
            _basketRepository.Verify(x => x.CreateAsync(It.Is<Basket>(b=>b.UserId==userId)),Times.Never);
           
        }
        [Fact]
        public async Task Login_ShouldThrowKeyNotFoundException_WhenEmailIsNotFound()
        {
            //arrange
            var loginDtoRequest = new LoginDtoRequest
            {
                Email = "emre@live.be",
                Password = "Emre1997",
            };
            
            _userManagerMock.Setup(x => x.FindByEmailAsync("oz@live.be")).ReturnsAsync((ApplicationUser)null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(() => _authService.Login(loginDtoRequest));
            Assert.NotNull(error);
            Assert.Equal("user doesn't exist", error.Result.Message);
        }
        [Fact]
        public async Task Login_ShouldThrowUnauthorizedException_WhenPasswordIsWrong()
        {
            //arrange
            var loginDtoRequest = new LoginDtoRequest
            {
                Email = "emre@live.be",
                Password = "Emre1997",
            };
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
            };
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(loginDtoRequest.Email)).ReturnsAsync(user);
            _userManagerMock.Setup(x => x.CheckPasswordAsync(user, "EmreOWZan")).ReturnsAsync(false);
            //act assert
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(() => _authService.Login(loginDtoRequest));
            Assert.NotNull(error);
            Assert.Equal("wrong password", error.Result.Message);
        }

        //refresh-Token
        [Fact]
        public async Task RefreshToken_ShouldReturnLoginDtoResponse_WhenRefreshTokenAndEmailIsValid()
        {
            // arrange
             var refreshTokenDtoRequest = new RefreshTokenDtoRequest
            {
                Email = "emre@live.be",
                RefreshToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8Elolcai",    
            };
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
                Refreshtoken="JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8Elolcai",
                RefreshTokenExpireTime=DateTime.UtcNow.AddHours(12),
            };
             var loginDtoResponse = new LoginDtoResponse
            {
                AccessToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai",
                refreshToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai",
                Message = "Success",
                
            };
             _tokenServiceMock.Setup(x => x.GenerateToken(user)).ReturnsAsync(loginDtoResponse.AccessToken);
             _tokenServiceMock.Setup(x => x.GenerateRefreshToken()).Returns(loginDtoResponse.refreshToken);
             _userManagerMock.Setup(x => x.FindByEmailAsync(refreshTokenDtoRequest.Email)).ReturnsAsync(user);
            // act
            var result = await _authService.RefreshToken(refreshTokenDtoRequest);
            // assert
            Assert.NotNull(result);
            Assert.Equal("Success", result.Message);
            
            Assert.Equal("JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai", result.AccessToken);
            Assert.Equal("JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8EAcgai", result.refreshToken);
        }
        [Fact]
        public async Task RefreshToken_ShouldThrowUnauthorizedException_WhenRefreshTokenIsNotValid()
        {
            // arrange
             var refreshTokenDtoRequest = new RefreshTokenDtoRequest
            {
                Email = "emre@live.be",
                RefreshToken = "sdhddfhediu23wher8iehfiehdiowedhjwe89du2w3q98dfyu3e49r8eu2900eu2309r23",    
            };
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
                Refreshtoken="JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8Elolcai",
                RefreshTokenExpireTime=DateTime.UtcNow.AddHours(12),
            };   
             _userManagerMock.Setup(x => x.FindByEmailAsync(refreshTokenDtoRequest.Email)).ReturnsAsync(user);
            // act
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(()=>_authService.RefreshToken(refreshTokenDtoRequest));
            // assert
            Assert.NotNull(error);
            Assert.Equal("Invalid or expired refresh token", error.Result.Message);
        }
        [Fact]
        public async Task RefreshToken_ShouldThrowUnauthorizedException_WhenRefreshTokenIsExpired()
        {
            // arrange
             var refreshTokenDtoRequest = new RefreshTokenDtoRequest
            {
                Email = "emre@live.be",
                RefreshToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8Elolcai",    
            };
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",
                Refreshtoken="JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8Elolcai",
                RefreshTokenExpireTime=DateTime.UtcNow.AddDays(-12),
            };   
             _userManagerMock.Setup(x => x.FindByEmailAsync(refreshTokenDtoRequest.Email)).ReturnsAsync(user);
            // act
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(()=>_authService.RefreshToken(refreshTokenDtoRequest));
            // assert
            Assert.NotNull(error);
            Assert.Equal("Invalid or expired refresh token", error.Result.Message);
        }
        [Fact]
        public async Task RefreshToken_ShouldThrowKeyNotFoundException_WhenEmailIsNotFound()
        {
            // arrange
             var refreshTokenDtoRequest = new RefreshTokenDtoRequest
            {
                Email = "emre@live.be",
                RefreshToken = "JkqU9yzXhHDQxLv3Pc9ZBKeWe3QmiH3QrFvX47fX57u6MRWtCzXsvSSER8Elolcai",    
            };
             _userManagerMock.Setup(x => x.FindByEmailAsync("em@live.be")).ReturnsAsync((ApplicationUser) null);
            // act
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=>_authService.RefreshToken(refreshTokenDtoRequest));
            // assert
            Assert.NotNull(error);
            Assert.Equal("user doesn't exist", error.Result.Message); 
        }

        //make-admin
        [Fact]
        public async Task MakeAdmin_ShouldReturnSuccessMessage_WhenEmailIsValidAndROleIsUser()
        {
            // Arrange
            var email = "emre-1030@live.be";
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",    
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _userManagerMock.Setup(x=>x.GetRolesAsync(user)).ReturnsAsync(new List<string> { UserRoles.USER });
        
            // act
             var result = await _authService.MakeAdmin(email);
            // assert
            Assert.NotNull(result);
            Assert.Equal("user is now admin", result);
            _userManagerMock.Verify(um => um.AddToRoleAsync(user, UserRoles.ADMIN), Times.Once);
            _userManagerMock.Verify(um => um.RemoveFromRoleAsync(user, UserRoles.USER), Times.Once);
        }
        [Fact]
        public async void MakeAdmin_ShouldThrowException_WhenRoleIsAlreadyAdmin()
        {
            // Arrange
            var email = "emre-1030@live.be";
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",    
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync(email)).ReturnsAsync(user);
            _userManagerMock.Setup(x=>x.GetRolesAsync(user)).ReturnsAsync(new List<string> { UserRoles.ADMIN });
        
            // act
             var error = await Assert.ThrowsAsync<Exception>(()=>_authService.MakeAdmin(email));
            // assert
            Assert.NotNull(error);
            Assert.Equal("user is already admin", error.Message);
            _userManagerMock.Verify(um => um.AddToRoleAsync(user, UserRoles.ADMIN), Times.Never);
            _userManagerMock.Verify(um => um.RemoveFromRoleAsync(user, UserRoles.USER), Times.Never);
        }
        [Fact]
        public async void MakeAdmin_ShouldThrowKeyNotFoundException_WhenEmailIsNotFound()
        {
            // Arrange
            var email = "emre-1030@live.be";
            var user = new ApplicationUser
            {
                Id = "emre",
                Email = "emre@live.be",
                FirstName = "Emre",
                LastName = "Emre",    
            };
            _userManagerMock.Setup(x => x.FindByEmailAsync("emal@live.be")).ReturnsAsync((ApplicationUser) null);
            // act
             var error = await Assert.ThrowsAsync<KeyNotFoundException>(()=>_authService.MakeAdmin(email));
            // assert
            Assert.NotNull(error);
            Assert.Equal("invalid username or username not found", error.Message);
            _userManagerMock.Verify(um => um.AddToRoleAsync(user, UserRoles.ADMIN), Times.Never);
            _userManagerMock.Verify(um => um.RemoveFromRoleAsync(user, UserRoles.USER), Times.Never);
        }
    }
}