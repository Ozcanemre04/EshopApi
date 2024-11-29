using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Request.Auth
{
    public class RefreshTokenDtoRequest
    {
        [Required(ErrorMessage = "email is required")]
        [EmailAddress(ErrorMessage = "should be valid email adress")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "refreshToken is required")]
        [MinLength(50,ErrorMessage = "min length should be 50 character")]
        public string? RefreshToken { get; set; }
    }
}