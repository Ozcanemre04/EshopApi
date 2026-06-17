using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Domain.Entities;

namespace Eshop.Application.Dtos.Response.BasketProduct
{
    public class BasketProductDtoResponse : BaseDto
    {
        public required int Quantity { get; set; }
        public decimal TotalPrice {get;set;}
        public decimal Price {get;set;}
        public  long ProductId { get; set; }
        public  string? ProductName { get; set; }
        public  string? Image { get; set; }
        public required int Stock { get; set; }
        public required long BasketId { get; set; }

    }
}