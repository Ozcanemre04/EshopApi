using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Domain.Entities;

namespace Eshop.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
        public async Task<ProductDtoResponse> CreateProductAsync(ProductDtoCreateRequest productDtoCreateRequest)
        {
            var category = await _categoryRepository.GetOneAsync(productDtoCreateRequest.CategoryId);
            if (category == null)
            {
                throw new KeyNotFoundException("category with this id doesn't exist");
            }
            var product = _mapper.Map<Product>(productDtoCreateRequest);
            await _productRepository.CreateAsync(product);
            return _mapper.Map<ProductDtoResponse>(product);
        }

        public async Task<string> DeleteProductAsync(long id)
        {
            return await _productRepository.DeleteAsync(id) ? "product is deleted" : throw new KeyNotFoundException("product is not found");

        }

        public async Task<PageDto<ProductDtoResponse>> GetAllProductAsync(int pageNumber, int pageSize,string category,string? search,string? order_type,bool asc)
        { 
            var count = await _productRepository.Count(category,search);
            var getcategory = await _categoryRepository.GetOneByNameAsync(category);
            var productRepository = await _productRepository.GetAllAsync(pageNumber, pageSize,category,search,order_type,asc);
            int number_of_Product = productRepository.Count();
            var dto = productRepository.Select(product => _mapper.Map<ProductDtoResponse>(product)).ToList();
            PageDto<ProductDtoResponse> pageResponse = new PageDto<ProductDtoResponse>
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize),
                TotalRecords = count,
                Data = dto
            };
            return pageResponse;
        }

        public async Task<ProductDtoResponse> GetOneProductAsync(long id)
        {
            var productRepository = await _productRepository.GetOneAsync(id) ?? throw new KeyNotFoundException($"product with id: {id} is not found");
            var dto = _mapper.Map<ProductDtoResponse>(productRepository);
            return dto;
        }

        public async Task<ProductDtoResponse> UpdateProductAsync(ProductDtoUpdateRequest productDtoUpdateRequest, long id)
        {
            var product = await _productRepository.GetOneAsync(id) ?? throw new KeyNotFoundException("product is not found");
            product.UpdatedDate = DateTime.UtcNow;
            _mapper.Map(productDtoUpdateRequest, product);
            await _productRepository.UpdateAsync(product);
            return _mapper.Map<ProductDtoResponse>(product);
        }
    }


}