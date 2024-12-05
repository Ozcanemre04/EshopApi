using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Product;

namespace Eshop.Application.Test.Fixture
{
    public class ProductDtoFixture
    {
         public static IEnumerable<ProductDtoResponse> getAllProducts()
        {
           return new List<ProductDtoResponse>
             {
                new ProductDtoResponse {
                    Id = 1,
                    Name = "iphone",
                    Description = "This is a fantastic product you should buy.",
                    Price = 159.56m,
                    Stock = 10,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    Image ="hello",
                    CategoryName="Smartphone"
                },
                new ProductDtoResponse {
                    Id = 2,
                    Name = "samsung",
                    Description = "This is a fantastic product you should buy.",
                    Price = 235.43m,
                    Stock = 10,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    Image ="hello",
                    CategoryName="Smartphone"
                },
                new ProductDtoResponse {
                    Id = 2,
                    Name = "samsung",
                    Description = "This is a fantastic product you should buy.",
                    Price = 235.43m,
                    Stock = 10,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    Image ="hello",
                    CategoryName="Books"
                }
             };
        }
    }
}