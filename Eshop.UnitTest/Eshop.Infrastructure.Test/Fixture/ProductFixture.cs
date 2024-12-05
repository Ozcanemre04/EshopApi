using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Test.Fixture
{
    public class ProductFixture
    {
         public static IEnumerable<Product> getAllProducts()
        {
           return new List<Product>
             {
                new Product {
                    Id = 1,
                    Name = "iphone",
                    Description = "This is a fantastic product you should buy.",
                    Price = 159.56m,
                    Stock = 10,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    Image ="hello",
                    category=CategoryFixture.CategoryList().First(),
                    
                },
                new Product {
                    Id = 2,
                    Name = "samsung",
                    Description = "This is a fantastic product you should buy.",
                    Price = 235.43m,
                    Stock = 10,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    Image ="hello",
                    category=CategoryFixture.CategoryList().First(),
                },
                new Product {
                    Id = 3,
                    Name = "samsung",
                    Description = "This is a fantastic product you should buy.",
                    Price = 235.43m,
                    Stock = 10,
                    CategoryId = 1,
                    CreatedDate = DateTime.Now,
                    Image ="hello",
                    category=CategoryFixture.CategoryList().Last(),
                }
             };
        }
    }
}