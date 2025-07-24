using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.BasketProduct;
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
            return await _appDbContext.Baskets.AsNoTracking().FirstOrDefaultAsync(x => x.UserId == id);
        }
        
        // #nullable disable
        public async Task<BasketDtoResponse> GetAllAsync(string userid)
        {
            return await _appDbContext.Baskets.AsNoTracking().Include(b => b.BasketProducts).ThenInclude(bp => bp.Product)
            .Where(b => b.UserId == userid)
            .Select(b => new BasketDtoResponse
            {
                UserId = b.UserId,
                BasketProductDtoResponses = b.BasketProducts.Where(x => x.Product != null).Select(x => new BasketProductDtoResponse
                {
                    Id = x.Id,
                    Quantity = x.Quantity,
                    TotalPrice = x.TotalPrice,
                    ProductId = x.ProductId,
                    BasketId = x.BasketId,
                    ProductName = x.Product.Name,
                    Image = x.Product.Image,
                    Stock = x.Product.Stock,
                    CreatedDate = x.CreatedDate,
                    Price = x.Product.Price,

                }).ToList() ?? new List<BasketProductDtoResponse>()
                }).FirstOrDefaultAsync();
        }
    }
}