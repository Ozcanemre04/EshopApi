

using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize,string category,string search,string? order_type,bool asc);
        Task<int> Count(string category);
        Task<Product> GetOneAsync(long id);
        Task<Product> CreateAsync(Product product);
        Task<bool> DeleteAsync(long id);
        Task<Product> UpdateAsync(Product product);
    }
}