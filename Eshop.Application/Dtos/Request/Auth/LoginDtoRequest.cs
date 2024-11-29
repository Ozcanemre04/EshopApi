using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Request.Auth
{
    public class LoginDtoRequest
    {
        public  string? Email {get; set;}
        public  string? Password {get; set;}
    }
}