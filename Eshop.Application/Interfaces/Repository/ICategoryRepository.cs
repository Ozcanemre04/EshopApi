using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> GetOneAsync(long id);
        Task<Category> GetOneByNameAsync(string category);
        Task<Category> CreateAsync(Category category);
        Task<bool> DeleteAsync(long id);
        Task<Category> UpdateAsync(Category category);
    }
}