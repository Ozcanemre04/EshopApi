using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Repository;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Eshop.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        
        private IQueryable<Product> filterProduct(string category, string search)
        {
            var query = _appDbContext.Products.Where(x => category == "All" || x.category.CategoryName == category);
            query = query.Where(x => string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search.ToLower()));
            return query;
        }
        public async Task<int> Count(string category,string search)
        {  
            IQueryable<Product> query = filterProduct(category, search);
            return await query.CountAsync();
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

        public async Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize, string category, string search,
        string? order_type,bool asc)
        {
            IQueryable<Product> query = filterProduct(category, search)
                                        .Skip((pageNumber - 1) * pageSize).Take(pageSize).Include(x => x.category);
            switch (order_type)
            {
                case "name":
                 query=asc?query.OrderBy(x => x.Name):query.OrderByDescending(x => x.Name);
                    break;
                case "price":
                    query=asc?query.OrderBy(x => x.Price):query.OrderByDescending(x => x.Price);
                    break;
                default:
                   query=query.OrderBy(x => x.Id);
                    break;
            }
            return await query.ToListAsync();
        }

        public async Task<Product> GetOneAsync(long id)
        {
            return await _appDbContext.Products.AsNoTracking().Include(x => x.category).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _appDbContext.Update(product);
            await _appDbContext.SaveChangesAsync();
            return product;
        }
    }
}