using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Service;

namespace Eshop.Application.Services
{
    public class ProductServiceDecorator : IProductService
    {
        private readonly IProductService _productService;

        public ProductServiceDecorator(IProductService productService)
        {
            _productService = productService;
        }

        public virtual async Task<ProductDtoResponse> CreateProductAsync(ProductDtoCreateRequest productDtoCreateRequest)
        {
            return await _productService.CreateProductAsync(productDtoCreateRequest);
        }

        public virtual async Task<string> DeleteProductAsync(long id)
        {
            return await _productService.DeleteProductAsync(id);
        }

        public virtual async Task<PageDto<ProductDtoResponse>> GetAllProductAsync(int pageNumber, int pageSize, string category, string? search, string? order_type, bool asc)
        {
            return await _productService.GetAllProductAsync(pageNumber, pageSize, category, search, order_type, asc);
        }

        public virtual async Task<ProductDtoResponse> GetOneProductAsync(long id)
        {
            return await _productService.GetOneProductAsync(id);
        }

        public virtual async Task<ProductDtoResponse> UpdateProductAsync(ProductDtoUpdateRequest productDtoUpdateRequest, long id)
        {
            return await _productService.UpdateProductAsync(productDtoUpdateRequest, id);
        }
    }
}