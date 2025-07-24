using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.BasketProduct;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IBasketRepository
    {
        
        Task<Basket> CreateAsync(Basket basket);
        Task<Basket> GetOneAsync(string id);
        Task<BasketDtoResponse> GetAllAsync(string userid);
       
    }
}