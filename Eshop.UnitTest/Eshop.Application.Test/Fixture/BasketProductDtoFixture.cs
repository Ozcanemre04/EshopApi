using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.BasketProduct;

namespace Eshop.Application.Test.Fixture
{
    public class BasketProductDtoFixture
    {
        public static IEnumerable<BasketProductDtoResponse> AllProductInBasket()
        {
            return new List<BasketProductDtoResponse>{
                new BasketProductDtoResponse(){
                     Id = 1,
                     CreatedDate =DateTime.Now,
                     Quantity =1,
                     TotalPrice = 159.56m,
                     ProductId =1,
                     BasketId =1,
                     Stock=120,
                     Image="image",
                     ProductName="necklace"
                },
                new BasketProductDtoResponse(){
                     Id = 2,
                     CreatedDate =DateTime.Now,
                     Quantity =1,
                     TotalPrice = 100,
                     ProductId =1,
                     BasketId =1,
                     Stock=120,
                     Image="image",
                     ProductName="necklace"
                },
            };
        }
    }
}