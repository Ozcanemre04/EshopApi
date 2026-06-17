using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Auth;
using FluentValidation;

namespace Eshop.Application.Validations.Auth
{
    public class LoginDtoValidation : AbstractValidator<LoginDtoRequest>
    {
        public LoginDtoValidation()
        {
            RuleFor(request => request.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email address.");

            RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(5)
            .WithMessage("Password must be at least 5 characters long.");
        }
    }
}