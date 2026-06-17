using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Response.Auth;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Identity;
using Eshop.Infrastructure.Interface;
using Eshop.Infrastructure.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Eshop.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly IBasketRepository _basketRepository;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager,
        IMapper mapper,ITokenService tokenService,IBasketRepository basketRepository)
        {
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
            _basketRepository = basketRepository;
        }
        public async Task<LoginDtoResponse> Login(LoginDtoRequest loginDtoRequest)
        {
            var response = new LoginDtoResponse();
            var Identity = await _userManager.FindByEmailAsync(loginDtoRequest.Email!) ?? throw new KeyNotFoundException("user doesn't exist");

            var verifyPassword = await _userManager.CheckPasswordAsync(Identity, loginDtoRequest.Password!);
            if (!verifyPassword)
            {
                throw new UnauthorizedAccessException("wrong password");
            };
            var basketRepo = await _basketRepository.GetOneAsync(Identity.Id);
            if (basketRepo == null)
            {
                var basket = new Basket
                {
                    UserId = Identity.Id,

                };
                await _basketRepository.CreateAsync(basket);
            }

            string token = await _tokenService.GenerateToken(Identity);
            response.Message = "Success";
            response.AccessToken = token;
            response.refreshToken = _tokenService.GenerateRefreshToken();
            

            Identity.Refreshtoken = response.refreshToken;
            Identity.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(12);
            await _userManager.UpdateAsync(Identity);
            return response;
        }

        public async Task<LoginDtoResponse> RefreshToken(RefreshTokenDtoRequest refreshTokenDtoRequest)
        {
            
           var Identity = await _userManager.FindByEmailAsync(refreshTokenDtoRequest.Email) ?? throw new KeyNotFoundException("user doesn't exist");
           var response = new LoginDtoResponse();

            
            if (Identity is null || Identity.Refreshtoken != refreshTokenDtoRequest.RefreshToken || Identity.RefreshTokenExpireTime < DateTime.Now)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token");
            }
            response.Message = "Success";
            response.AccessToken = await _tokenService.GenerateToken(Identity);
            response.refreshToken = _tokenService.GenerateRefreshToken();

            Identity.Refreshtoken = response.refreshToken;
            Identity.RefreshTokenExpireTime = DateTime.UtcNow.AddHours(12);
            await _userManager.UpdateAsync(Identity);

            return response;
        }

        public async Task<RegisterDtoResponse> Register(RegisterDtoRequest registerDtoRequest)
        {
            var userAlreadyExist = await _userManager.FindByEmailAsync(registerDtoRequest.Email!);
            var UsernameTaken = await _userManager.FindByNameAsync(registerDtoRequest.Username!);

            if (userAlreadyExist != null)
            {
                throw new Exception("email already exist");
            }
            if (UsernameTaken != null)
            {
                throw new Exception("UserName already taken");
            }

            var identityUser = _mapper.Map<ApplicationUser>(registerDtoRequest);
            var result = await _userManager.CreateAsync(identityUser, registerDtoRequest.Password!);
            if (!result.Succeeded)
            {
                var errorString = "User Creation Failed Because: ";
                foreach (var error in result.Errors)
                {
                    errorString += " # " + error.Description;
                }
                throw new Exception(errorString);
            }
            await _userManager.AddToRoleAsync(identityUser, UserRoles.USER);
            var registerResponseDto = _mapper.Map<RegisterDtoResponse>(identityUser);
            return registerResponseDto;
        }


        public async Task<string> MakeAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new KeyNotFoundException("invalid username or username not found");

            var role = await _userManager.GetRolesAsync(user);
            if (role.Contains(UserRoles.ADMIN))
            {
                throw new Exception("user is already admin");
            }
            await _userManager.AddToRoleAsync(user, UserRoles.ADMIN);
            await _userManager.RemoveFromRoleAsync(user, UserRoles.USER);
            return "user is now admin";
        }

    }
}