using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Category;
using Eshop.Application.Dtos.Response.Category;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Mapper;
using Eshop.Application.Services;
using Eshop.Application.Test.Fixture;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Test.Fixture;
using Moq;

namespace Eshop.Application.Test.Service
{
    public class CategoryServiceTest
    {
       
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly IMapper _mapper;
        private readonly CategoryService _categoryService;
        

        public CategoryServiceTest()
        {
            
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
            _categoryService = new CategoryService(_categoryRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ShouldReturnListOfCategoryDtoResponse()
        {
            //arrange
            _categoryRepositoryMock.Setup(p => p.GetAllAsync()).ReturnsAsync(CategoryFixture.CategoryList());
            //act
            var result = await _categoryService.GetAllCategoriesAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<CategoryDtoResponse>>(result);
            Assert.Equal(CategoryFixture.CategoryList().Count(), result.Count());
            
            

        }
        [Fact]
        public async Task GetOneCategoryAsync_ShouldReturnCategoryDtoResponse_whenCategoryExist()
        {
            //arrange
            long categoryId = 1;
            Category category = CategoryFixture.CategoryList().FirstOrDefault(x => x.Id == categoryId)!;
            CategoryDtoResponse categoryDtoResponse = CategoryDtoFixture.CategoryList().FirstOrDefault(x => x.Id == categoryId)!;
            _categoryRepositoryMock.Setup(p => p.GetOneAsync(categoryId)).ReturnsAsync(category);
            //act
            var result = await _categoryService.GetOneCategoryAsync(categoryId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<CategoryDtoResponse>(result);
            Assert.Equal(categoryDtoResponse.Name, result.Name);
            Assert.Equal(categoryDtoResponse.Id, result.Id);
            
        }
        [Fact]
        public async Task GetOneCategoryAsync_ShouldThrowKeyNotFoundException_whenCategoryDoesNotExist()
        {
            //arrange
            long categoryId = 999;
            _categoryRepositoryMock.Setup(p => p.GetOneAsync(categoryId)).ReturnsAsync((Category) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _categoryService.GetOneCategoryAsync(categoryId));
            Assert.Equal($"category with id: {categoryId} is not found", error.Result.Message);  
        }
        //delete
        [Fact]
        public async Task DeleteCategoryAsync_ShouldReturnMessage_whenCategoryExist()
        {
            //arrange
            long categoryId = 1;
            _categoryRepositoryMock.Setup(p => p.DeleteAsync(categoryId)).ReturnsAsync(true);
            //act
            var result = await _categoryService.DeleteCategoryAsync(categoryId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("category is deleted",result);  
        }
        [Fact]
        public async Task DeleteCategoryAsync_ShouldThrowKeyNotFoundException_whenCategoryDoesNotExist()
        {
            //arrange
            long categoryId = 999;
            _categoryRepositoryMock.Setup(p => p.DeleteAsync(categoryId)).ReturnsAsync(false);
            //act assert
            var error= Assert.ThrowsAsync<KeyNotFoundException>(()=> _categoryService.DeleteCategoryAsync(categoryId));
            Assert.Equal($"category with id: {categoryId} is not found", error.Result.Message);   
        }
        //create
        [Fact]
        public async Task CreateCategoryAsync_ShouldReturnCategoryDtoResponse_WhenCategoryExists()
        {
            //arrange
            var categoryDtoCreate = new CategoryDtoCreateRequest
            {
                Name = "Smartphone",
            };
            var CategoryCreate = new Category
            {
                Id=1,
                CategoryName = "Smartphone",     
            };
            _categoryRepositoryMock.Setup(c=>c.CreateAsync(It.IsAny<Category>())).ReturnsAsync(CategoryCreate);

            //act
            var result = await _categoryService.CreateCategoryAsync(categoryDtoCreate);
            //assert
            Assert.NotNull(result);
            Assert.IsType<CategoryDtoResponse>(result);
            Assert.Equal("Smartphone", result.Name);
            _categoryRepositoryMock.Verify(um => um.CreateAsync(It.Is<Category>(x => x.CategoryName == "Smartphone")), Times.Once);
            
        }
       
        //update
        [Fact]
        public async Task UpdateCategoryAsync_ShouldReturnCategoryDtoResponse_WhenCategoryExists()
        {
            //arrange
            var category = CategoryFixture.CategoryList().FirstOrDefault(x=>x.CategoryName=="Smartphone");
            var categoryDto = CategoryDtoFixture.CategoryList().FirstOrDefault(x=>x.Name=="Smartphone");
            var categoryDtoUpdate = new CategoryDtoUpdateRequest
            {
                Name = "Smartphonee",
            };
            var categoryUpdate = new Category
            {
                Id=1,
                CategoryName = "Smartphonee",   
            };
           
            _categoryRepositoryMock.Setup(p => p.GetOneAsync(category.Id)).ReturnsAsync(category);
            _categoryRepositoryMock.Setup(p => p.UpdateAsync(categoryUpdate)).ReturnsAsync(categoryUpdate);
            //act
            var result = await _categoryService.UpdateCategoryAsync(categoryDtoUpdate,category.Id);
            //assert
            Assert.NotNull(result);
            Assert.IsType<CategoryDtoResponse>(result);
            Assert.Equal(categoryDtoUpdate.Name, result.Name);
            Assert.Equal(categoryUpdate.Id, result.Id);
            _categoryRepositoryMock.Verify(um => um.UpdateAsync(It.Is<Category>(x => x.CategoryName == "Smartphonee")), Times.Once);

           
        }
        [Fact]
        public async Task UpdateCategoryAsync_ShouldThrowKeyNotFoundException_WhenCategoryDoesNotExists()
        {
            //arrange
            var categoryId = 999;
            var categoryDtoUpdate = new CategoryDtoUpdateRequest{};
            _categoryRepositoryMock.Setup(c=>c.GetOneAsync(categoryId)).ReturnsAsync((Category) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _categoryService.UpdateCategoryAsync(categoryDtoUpdate,categoryId));
            Assert.Equal("category is not found", error.Result.Message);
            
        }
    }
}