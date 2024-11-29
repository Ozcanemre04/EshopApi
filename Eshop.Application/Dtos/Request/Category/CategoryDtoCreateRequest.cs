using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Request.Category
{
    public class CategoryDtoCreateRequest
    {
        [MinLength(3, ErrorMessage = "Your category name must be at least 3 characters long.")]
        [MaxLength(20, ErrorMessage = "the Name of category exceeds the maximum character limit of 20.")]
        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

    }
}