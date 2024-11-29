using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Category;

namespace Eshop.Application.Test.Fixture
{
    public class CategoryDtoFixture
    {
        public static IEnumerable<CategoryDtoResponse> CategoryList()
        {
            return new List<CategoryDtoResponse>{
                new CategoryDtoResponse{
                    Id = 1,
                    Name = "Smartphone",
                    CreatedDate = DateTime.Now,
                },
                new CategoryDtoResponse{
                    Id = 2,
                    Name = "Books",
                    CreatedDate = DateTime.Now,
                }
            };
        }
    }
}