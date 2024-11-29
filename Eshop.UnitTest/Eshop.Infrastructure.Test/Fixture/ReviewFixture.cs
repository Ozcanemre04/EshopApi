using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Test.Fixture
{
    public class ReviewFixture
    {
        public static IEnumerable<Review> ReviewList()
        {
            return new List<Review>{
                new Review{
                    Id = 1,
                    Stars=4,
                    Reviews="This is a fantastic product you should buy.This is a fantastic product you should buy.",
                    UserId="bad206e0-3980-4746-893b-80afc748dfea",
                    ProductId=1,
                    Product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id == 1),
                    CreatedDate = DateTime.Now,
                    
                },
                new Review{
                    Id = 2,
                    Stars=3,
                    Reviews="This is a fantastic product you should buy.This is a fantastic product you should buy.",
                    UserId="bad206e0-3980-4746-893b-80afc748dfea",
                    ProductId=2,
                    Product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id == 2),
                    CreatedDate = DateTime.Now,   
                },
            };
        }
    }
}