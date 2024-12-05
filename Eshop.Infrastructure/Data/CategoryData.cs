using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Data
{
    public class CategoryData
    {
        public static List<Category> CategoriesList()
        {
            return new List<Category>
            {
                new Category {
                    Id = 1,
                    CategoryName = "Men's clothing"
                },
                new Category {
                    Id = 2,
                    CategoryName = "Women's clothing"
                },
                new Category {
                    Id = 3,
                    CategoryName = "Jewelery"
                },
                new Category {
                    Id = 4,
                    CategoryName = "Electronics"
                }
            };
        }
    }
}