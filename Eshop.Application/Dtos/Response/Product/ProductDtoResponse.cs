using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Domain.Entities;

namespace Eshop.Application.Dtos.Response.Product
{
    public class ProductDtoResponse : BaseDto
    {

        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required int Stock { get; set; }
        public required string Image { get; set; }
        public required string CategoryName { get; set; }
        public required long CategoryId { get; set; }
        public double? Ratings { get; set; }
        
       
    }
}