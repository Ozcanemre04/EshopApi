using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IBasketRepository
    {
        
        Task<Basket> CreateAsync(Basket basket);
        Task<Basket> GetOneAsync(string id);
        Task<Basket> GetAllAsync(string userid);
       
    }
}