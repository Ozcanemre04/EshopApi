using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.BasketProduct;

namespace Eshop.Application.Test.Fixture
{
    public class BasketDtoFixture
    {
         public static BasketDtoResponse AllProductInBasket(){
            return new BasketDtoResponse{
                
                UserId= "bad206e0-3980-4746-893b-80afc748dfea",
                BasketProductDtoResponses= BasketProductDtoFixture.AllProductInBasket(),
            };
    }
    }
}