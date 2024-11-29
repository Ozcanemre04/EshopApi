using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Category;
using Eshop.Application.Dtos.Response.Category;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Domain.Entities;

namespace Eshop.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<CategoryDtoResponse> CreateCategoryAsync(CategoryDtoCreateRequest categoryDtoCreateRequest)
        {
            var category = _mapper.Map<Category>(categoryDtoCreateRequest);
            await _categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryDtoResponse>(category);
        }


        public async Task<string> DeleteCategoryAsync(long id)
        {
            return await _categoryRepository.DeleteAsync(id) ? "category is deleted" : throw new KeyNotFoundException($"category with id: {id} is not found");

        }

        public async Task<IEnumerable<CategoryDtoResponse>> GetAllCategoriesAsync()
        {
            var CategoryRepository = await _categoryRepository.GetAllAsync();
            var dto = CategoryRepository.Select(category => _mapper.Map<CategoryDtoResponse>(category)).ToList();
            return dto;
        }

        public async Task<CategoryDtoResponse> GetOneCategoryAsync(long id)
        {
            var categoryRepository = await _categoryRepository.GetOneAsync(id) ?? throw new KeyNotFoundException($"category with id: {id} is not found");
            var dto = _mapper.Map<CategoryDtoResponse>(categoryRepository);
            return dto;
        }

        public async Task<CategoryDtoResponse> UpdateCategoryAsync(CategoryDtoUpdateRequest categoryDtoUpdateRequest, long id)
        {
            var category = await _categoryRepository.GetOneAsync(id) ?? throw new KeyNotFoundException("category is not found");
            category.UpdatedDate = DateTime.UtcNow;
            _mapper.Map(categoryDtoUpdateRequest, category);
            await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryDtoResponse>(category);

        }
    }
}