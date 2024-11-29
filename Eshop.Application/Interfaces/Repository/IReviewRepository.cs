using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IReviewRepository
    {
        Task<IEnumerable<Review>> GetAllAsync();
        Task<Review> GetOneAsync(long id);
        Task<Review> CreateAsync(Review review);
        Task<bool> DeleteAsync(long id);
        Task<Review> UpdateAsync(Review review);
    }
}