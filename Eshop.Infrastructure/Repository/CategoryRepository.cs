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
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<Category> CreateAsync(Category category)
        {
            await _appDbContext.Categories.AddAsync(category);
            await _appDbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var category = await GetOneAsync(id);
            if (category == null)
            {
                return false;
            }
            _appDbContext.Remove(category);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _appDbContext.Categories.OrderBy(x => x.Id).ToListAsync();

        }

        public async Task<Category> GetOneAsync(long id)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Category> GetOneByNameAsync(string name)
        {
            return await _appDbContext.Categories.FirstOrDefaultAsync(x => x.CategoryName == name);
        }

        public async Task<Category> UpdateAsync(Category category)
        {
            _appDbContext.Update(category);
            await _appDbContext.SaveChangesAsync();
            return category;
        }
    }
}