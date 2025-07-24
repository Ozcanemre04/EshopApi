using System;
using Eshop.Application.Dtos.Response.BasketProduct;

namespace Eshop.Infrastructure.Test.Fixture;

public class BasketDtoFixtureInfra
{
    public static BasketDtoResponse AllProductInBasket(){
            return new BasketDtoResponse{
                
                UserId= "bad206e0-3980-4746-893b-80afc748dfea",
                BasketProductDtoResponses= BasketProductDtoFixtureInfra.AllProductInBasket(),
            };
    }
}
