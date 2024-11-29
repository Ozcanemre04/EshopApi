using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Test.Fixture
{
    public class CategoryFixture
    {
         public static IEnumerable<Category> CategoryList()
        {
            return new List<Category>{
                new Category{
                    Id = 1,
                    CategoryName = "Smartphone",
                    CreatedDate = DateTime.Now,
                },
                new Category{
                    Id = 2,
                    CategoryName = "Books",
                    CreatedDate = DateTime.Now,
                }
            };
        }
    }
}