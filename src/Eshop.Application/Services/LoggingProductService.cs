using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Service;
using Microsoft.Extensions.Logging;

namespace Eshop.Application.Services
{
    public class LoggingProductService : ProductServiceDecorator
    {
        private readonly ILogger<LoggingProductService> _logger;
        public LoggingProductService(IProductService productService, ILogger<LoggingProductService> logger) : base(productService)
        {
            _logger = logger;
        }

        public override async Task<ProductDtoResponse> CreateProductAsync(ProductDtoCreateRequest productDtoCreateRequest)
        {
            try
            {
               _logger.LogInformation("Creating a new product: {ProductName}", productDtoCreateRequest.Name);
               var result = await base.CreateProductAsync(productDtoCreateRequest);
               _logger.LogInformation("Product created successfully: {ProductId}", result.Id);
               return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a product: {ProductName}", productDtoCreateRequest.Name);
                throw; 
            }  
        }
          public override async Task<string> DeleteProductAsync(long id)
        {
            try
            {
                _logger.LogInformation("Deleting product with ID: {ProductId}", id);
                var result = await base.DeleteProductAsync(id);
                _logger.LogInformation("Product deleted successfully: {ProductId}", id);
                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting product: {ProductId}", id);
                throw;
            }
        }
         public override async Task<PageDto<ProductDtoResponse>> GetAllProductAsync(int pageNumber, int pageSize, string category, string? search, string? order_type, bool asc)
        {
            try
            {
                _logger.LogInformation("Fetching all products with pagination and filters.");
                var result = await base.GetAllProductAsync(pageNumber, pageSize, category, search, order_type, asc);
                _logger.LogInformation("Products fetched successfully.");
                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching products.");
                throw;
            }
        }
        public override async Task<ProductDtoResponse> GetOneProductAsync(long id)
        {
            try
            {
                _logger.LogInformation("Fetching product with ID: {ProductId}", id);
                var result = await base.GetOneProductAsync(id);
                _logger.LogInformation("Product fetched successfully: {ProductId}", id);
                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching product: {ProductId}", id);
                throw;
            }
        }
         public override async Task<ProductDtoResponse> UpdateProductAsync(ProductDtoUpdateRequest productDtoUpdateRequest, long id)
        {
            try
            {
                _logger.LogInformation("Updating product with ID: {ProductId}", id);
                var result = await base.UpdateProductAsync(productDtoUpdateRequest, id);
                _logger.LogInformation("Product updated successfully: {ProductId}", id);
                return result;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating product: {ProductId}", id);
                throw;
            }
        }
    }
}