using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Response.Auth
{
    public class RegisterDtoResponse
    {
        public  string? FirstName {get;set;}
        public  string? LastName {get;set;}
        public  string? Username {get;set;}
        public  string? Email {get;set;}
        
    }
}