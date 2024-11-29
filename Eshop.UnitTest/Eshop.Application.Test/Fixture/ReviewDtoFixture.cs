using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Review;

namespace Eshop.Application.Test.Fixture
{
    public class ReviewDtoFixture
    {
        public static IEnumerable<ReviewDtoResponse> ReviewList()
        {
            return new List<ReviewDtoResponse>{
                new ReviewDtoResponse{
                    Id = 1,
                    Stars=4,
                    Reviews="This is a fantastic product you should buy.This is a fantastic product you should buy.",
                    UserId="bad206e0-3980-4746-893b-80afc748dfea",
                    ProductId=1,
                    CreatedDate = DateTime.Now,
                    
                },
                new ReviewDtoResponse{
                    Id = 2,
                    Stars=3,
                    Reviews="This is a fantastic product you should buy.This is a fantastic product you should buy.",
                    UserId="bad206e0-3980-4746-893b-80afc748dfea",
                    ProductId=5,
                    CreatedDate = DateTime.Now,
                    
                },
            };
        }
    }
}