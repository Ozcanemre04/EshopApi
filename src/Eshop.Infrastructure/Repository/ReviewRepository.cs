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
    public class ReviewRepository : BaseRepository, IReviewRepository
    {
        public ReviewRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Review> CreateAsync(Review review)
        {
            await _appDbContext.Reviews.AddAsync(review);
            await _appDbContext.SaveChangesAsync();
            return review;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var review = await GetOneAsync(id);
            if (review == null)
            {
                return false;
            }
            _appDbContext.Remove(review);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetAllAsync(long productid)
        {
            return await _appDbContext.Reviews.AsNoTracking().Where(x=>x.ProductId==productid).ToListAsync();

        }
       

        public async Task<Review> GetOneAsync(long id)
        {
            return await _appDbContext.Reviews.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<Review> UpdateAsync(Review review)
        {
            _appDbContext.Update(review);
            await _appDbContext.SaveChangesAsync();
            return review;
        }
    }
}