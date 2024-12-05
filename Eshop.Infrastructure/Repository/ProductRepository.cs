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
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<int> Count(string category)
        {
            return await _appDbContext.Products.Where(x=> category=="All" || x.category.CategoryName == category).CountAsync();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var product = await GetOneAsync(id);
            if (product == null)
            {
                return false;
            }
            _appDbContext.Remove(product);
            await _appDbContext.SaveChangesAsync();
            return true;

        }

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize,string category)
        {
            return await _appDbContext.Products.Where(x=> category=="All" || x.category.CategoryName == category)
            .Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(x => x.category).OrderBy(x=>x.Id).ToListAsync();
        }

        public async Task<Product> GetOneAsync(long id)
        {
            return await _appDbContext.Products.Include(x => x.category).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _appDbContext.Update(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }
    }
}