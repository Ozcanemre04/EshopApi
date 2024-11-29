using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Review;
using FluentValidation;

namespace Eshop.Application.Validations.Reviews
{
    public class ReviewDtoCreateValidation : AbstractValidator<ReviewDtoCreateRequest>
    {
        public ReviewDtoCreateValidation()
        {
            RuleFor(request => request.Reviews)
            .NotNull()
            .WithMessage("Review text is required.")
            .Length(10, 400).WithMessage("character length of reviews must be between 10 and 200");

            RuleFor(request => request.Stars)
            .NotEmpty().WithMessage("Stars are required")
            .InclusiveBetween(1, 5)
            .WithMessage("Stars must be between 1 and 5.");

            RuleFor(request => request.ProductId)
            .GreaterThan(0)
            .WithMessage("Product ID must be greater than 0.");
        }
    }
}