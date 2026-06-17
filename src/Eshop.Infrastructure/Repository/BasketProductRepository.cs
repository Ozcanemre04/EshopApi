using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Interfaces.Repository;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Infrastructure.Repository
{
    public class BasketProductRepository : BaseRepository, IBasketProductRepository
    {
        public BasketProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<BasketProduct> CreateAsync(BasketProduct basketProduct)
        {
            await _appDbContext.BasketProducts.AddAsync(basketProduct);
            await _appDbContext.SaveChangesAsync();
            return basketProduct;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var BasketProduct = await GetOneAsync(id);
            if (BasketProduct == null)
            {
                return false;
            }
            _appDbContext.Remove(BasketProduct);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<BasketProduct> GetOneAsync(long id)
        {
            return await _appDbContext.BasketProducts.Include(x => x.Basket).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<BasketProduct> GetOneByBasketAndProductAsync(long basketId, long productId)
        {
            return await _appDbContext.BasketProducts.FirstOrDefaultAsync(x => x.BasketId == basketId && x.ProductId == productId);
        }

        public async Task<BasketProduct> UpdateAsync(BasketProduct basketProduct)
        {
            _appDbContext.Update(basketProduct);
            await _appDbContext.SaveChangesAsync();
            return basketProduct;
        }
    }
}