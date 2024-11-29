using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eshop.Application.Dtos.Response.BasketProduct
{
    public class BasketDtoResponse
    {
        public required string UserId { get; set; }
        
        public IEnumerable<BasketProductDtoResponse>? BasketProductDtoResponses{ get; set; }
    }
}