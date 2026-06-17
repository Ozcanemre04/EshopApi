using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IBasketProductRepository
    {
        Task<BasketProduct> GetOneAsync(long id);
        Task<BasketProduct> GetOneByBasketAndProductAsync(long basketId,long productId);
        Task<BasketProduct> CreateAsync(BasketProduct basketProduct);
        Task<bool> DeleteAsync(long id);
        Task<BasketProduct> UpdateAsync(BasketProduct basketProduct);
    }
}