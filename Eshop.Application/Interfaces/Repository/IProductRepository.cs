

using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync(int pageNumber, int pageSize);
        Task<int> Count();
        Task<Product> GetOneAsync(long id);
        Task<Product> CreateAsync(Product product);
        Task<bool> DeleteAsync(long id);
        Task<Product> UpdateAsync(Product product);
    }
}