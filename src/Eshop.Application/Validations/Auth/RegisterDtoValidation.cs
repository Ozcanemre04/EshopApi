using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Auth;
using FluentValidation;

namespace Eshop.Application.Validations.Auth
{
    public class RegisterDtoValidation : AbstractValidator<RegisterDtoRequest>
    {
        public RegisterDtoValidation()
        {
            RuleFor(request => request.FirstName)
            .NotEmpty()
            .WithMessage("First name is required.")
            .Length(3,20)
            .WithMessage("character length of firstname must be between 3 and 20")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("first letter should be uppercase and must not contain space and number");;

        RuleFor(request => request.LastName)
            .NotEmpty()
            .WithMessage("Last name is required.")
            .Length(3,20)
            .WithMessage("character length of lastname must be between 3 and 20")
            .Matches("^[A-Z][a-zA-Z]*$")
            .WithMessage("first letter should be uppercase and must not contain space and number");;

        RuleFor(request => request.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(3,20)
            .WithMessage("character length of username must be between 3 and 20")
            .Matches("^[A-Z][a-zA-Z0-9]*$")
            .WithMessage("first letter should be uppercase and must not contain space");

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