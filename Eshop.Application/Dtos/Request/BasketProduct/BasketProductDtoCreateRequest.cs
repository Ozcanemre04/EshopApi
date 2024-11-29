using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Request.BasketProduct
{
    public class BasketProductDtoCreateRequest
    {
        [Range(1, long.MaxValue,ErrorMessage = "ProductId for {0} must be between {1} and {2}.")]
        [Required(ErrorMessage = "ProductId is required")]
        public long ProductId { get; set; }
    }
}