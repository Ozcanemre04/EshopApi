using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Auth;
using Eshop.Application.Dtos.Response.Auth;

namespace Eshop.Application.Interfaces.Service
{
    public interface IAuthService
    {
        
        Task<RegisterDtoResponse> Register(RegisterDtoRequest registerDtoRequest);

        Task<LoginDtoResponse> Login(LoginDtoRequest loginDtoRequest);

        Task<LoginDtoResponse> RefreshToken(RefreshTokenDtoRequest refreshTokenDtoRequest);

        Task<string> MakeAdmin(string email);
        

        
    }
}