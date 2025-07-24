using System;
using Eshop.Application.Dtos.Response.BasketProduct;

namespace Eshop.Infrastructure.Test.Fixture;

public class BasketProductDtoFixtureInfra
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
