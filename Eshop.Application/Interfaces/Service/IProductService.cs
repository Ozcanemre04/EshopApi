using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;

namespace Eshop.Application.Interfaces.Service
{
    public interface IProductService
    {
        
        Task<PageDto<ProductDtoResponse>> GetAllProductAsync(int pageNumber, int pageSize);
        Task<ProductDtoResponse> GetOneProductAsync(long id);
        Task<ProductDtoResponse> CreateProductAsync(ProductDtoCreateRequest productDtoCreateRequest);
        Task<string> DeleteProductAsync(long id);
        Task<ProductDtoResponse> UpdateProductAsync(ProductDtoUpdateRequest productDtoUpdateRequest,long id);
    }
}