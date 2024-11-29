using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Product;
using FluentValidation;

namespace Eshop.Application.Validations.Products
{
    public class ProductDtoCreateValidation : AbstractValidator<ProductDtoCreateRequest>
    {
        public ProductDtoCreateValidation()
        {
            RuleFor(u=>u.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .Length(4,200).WithMessage("character length of product name must be between 3 and 200");

            RuleFor(u=>u.Description)
            .Length(5,200).WithMessage("character length of description must be between 3 and 200")
            .NotEmpty()
            .WithMessage("Description is required");

            RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");

        RuleFor(x => x.Stock)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Product stock must be non-negative.");

        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage("Product image is required.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .WithMessage("Product category id must be greater than 0");
            
        }
    }
}