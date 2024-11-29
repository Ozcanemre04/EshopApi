using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Infrastructure.Test.Fixture
{
    public class BasketProductFixture
    {
        public static ICollection<BasketProduct> AllProductInBasket()
        {
            return new List<BasketProduct>{
                new BasketProduct(){
                     Id = 1,
                     CreatedDate =DateTime.Now,
                     Quantity = 1,
                     TotalPrice = 159.56m,
                     ProductId = 1,
                     BasketId =1,
                     Basket = new Basket{
                        UserId="bad206e0-3980-4746-893b-80afc748dfea"
                     },
                },
                new BasketProduct(){
                     Id = 2,
                     CreatedDate =DateTime.Now,
                     Quantity =1,
                     TotalPrice = 100m,
                     ProductId =1,
                     BasketId =1,
                     Basket = new Basket{
                        UserId="bad206e0-3980-4746-873b-80afc748dfaa"
                     },
                     
                },
            };
        }
    }
}