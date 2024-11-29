using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Interfaces.Repository;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Eshop.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Eshop.Infrastructure.Repository
{
    public class BasketRepository : BaseRepository, IBasketRepository
    {
        public BasketRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Basket> CreateAsync(Basket basket)
        {
            await _appDbContext.AddAsync(basket);
            await _appDbContext.SaveChangesAsync();
            return basket;
        }

        public async Task<Basket> GetOneAsync(string id)
        {
            return await _appDbContext.Baskets.FirstOrDefaultAsync(x => x.UserId == id);
        }

        public async Task<Basket> GetAllAsync(string userid)
        {
            return await _appDbContext.Baskets.Include(x => x.BasketProducts).FirstOrDefaultAsync(p => p.UserId == userid);
        }
    }
}