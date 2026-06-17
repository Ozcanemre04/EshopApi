using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Application.Dtos.Request.Category;
using Eshop.Application.Dtos.Response.Category;
using Eshop.Domain.Entities;

namespace Eshop.Application.Interfaces.Service
{
    public interface ICategoryService
    {
        
        Task<IEnumerable<CategoryDtoResponse>> GetAllCategoriesAsync();
        Task<CategoryDtoResponse> GetOneCategoryAsync(long id);
        Task<CategoryDtoResponse> CreateCategoryAsync(CategoryDtoCreateRequest categoryDtoCreateRequest);
        Task<string> DeleteCategoryAsync(long id);
        Task<CategoryDtoResponse> UpdateCategoryAsync(CategoryDtoUpdateRequest categoryDtoUpdateRequest,long id);
    }
}