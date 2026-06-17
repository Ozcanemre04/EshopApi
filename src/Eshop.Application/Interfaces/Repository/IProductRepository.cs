

using Eshop.Application.Dtos.Response.Product;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllComplexAsync(int pageNumber, int pageSize,string category,string search,string? order_type,bool asc);
        Task<double?> AverageRatings(long id);
        Task<int> Count(string category,string search);
        Task<Product> GetOneAsync(long id);
        Task<Product> CreateAsync(Product product);
        Task<bool> DeleteAsync(long id);
        Task<Product> UpdateAsync(Product product);
    }
}