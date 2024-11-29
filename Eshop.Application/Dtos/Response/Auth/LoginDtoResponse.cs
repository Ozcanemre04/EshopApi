using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Response.Auth
{
    public class LoginDtoResponse
    {
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public string? refreshToken { get; set; }
        public string? Email {get; set;}
    }
}