using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Test.Fixture
{
    public class BasketFixture
    {
        public static IEnumerable<Basket> allBasket()
        {
            return new List<Basket>{
                new Basket 
                {   Id=1,
                    UserId = "bad206e0-3980-4746-893b-80afc748dfea", 
                    BasketProducts= BasketProductFixture.AllProductInBasket()
                },
                new Basket 
                {   Id=2,
                    UserId = "bad206e0-3980-4746-893b-80afc748dfea", 
                    BasketProducts= BasketProductFixture.AllProductInBasket()
                },
                
            };
        }
    }
}