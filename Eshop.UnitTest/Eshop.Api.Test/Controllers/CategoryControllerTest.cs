using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Api.Controllers;
using Eshop.Application.Dtos.Request.Category;
using Eshop.Application.Dtos.Response.Category;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace Eshop.Api.Test.Controllers
{
    public class CategoryControllerTest
    {
        private Mock<ICategoryService> _categoryService;
        private CategoryController _categoryController;

        public CategoryControllerTest()
        {
            _categoryService = new Mock<ICategoryService>();
            _categoryController = new CategoryController(_categoryService.Object);

        }
        //allCategories
        [Fact]
        public async void GetAllCategories_ReturnsOKResult_WithCategories()
        {
            //arrange
            _categoryService.Setup(t => t.GetAllCategoriesAsync()).ReturnsAsync(CategoryDtoFixture.CategoryList);
            //act
            var response = await _categoryController.GetAllCategories();
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var value = Assert.IsType<List<CategoryDtoResponse>>(okResult.Value);
            Assert.NotNull(value);
            Assert.Equal(2, value.Count);
        }
        //getOneCategory
        [Fact]
        public async void GetOneCategory_ProductExist_ReturnsOKResult_WithCategory()
        {
            //arrange
            long categoryId = 1;
            _categoryService.Setup(t => t.GetOneCategoryAsync(categoryId)).ReturnsAsync(CategoryDtoFixture.CategoryList().FirstOrDefault(x => x.Id == categoryId));

            //act
            var response = await _categoryController.GetOneCategory(categoryId);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<CategoryDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal(categoryId, Value.Id);

        }

        [Fact]
        public async void GetOneCategory_CategoryNotFound_ReturnsNotFoundResult()
        {
            //arrange
            long categoryId = 1;
            _categoryService.Setup(t => t.GetOneCategoryAsync(categoryId)).ThrowsAsync(new KeyNotFoundException($"category with id: {categoryId} is not found"));
            //act
            var response = await _categoryController.GetOneCategory(categoryId);

            //assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"category with id: {categoryId} is not found", notFoundResult.Value);

        }

        //deleteCategory
        [Fact]
        public async void DeleteCategory_CategoryExist_ReturnsOKResult()
        {
            //arrange
            long categoryId = 1;
            _categoryService.Setup(t => t.DeleteCategoryAsync(categoryId)).ReturnsAsync("category is deleted");
            //act
            var response = await _categoryController.DeleteCategory(categoryId);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<string>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal("category is deleted", Value);

        }
        [Fact]
        public async void DeleteCategory_CategoryNotFound_ReturnsNotFoundResult()
        {
            //arrange
            long categoryId = 1;
            _categoryService.Setup(t => t.DeleteCategoryAsync(categoryId)).ThrowsAsync(new KeyNotFoundException($"category with id: {categoryId} is not found"));

            //act
            var response = await _categoryController.DeleteCategory(categoryId);
            //assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"category with id: {categoryId} is not found", notFoundResult.Value);

        }
        //createCategory
        [Fact]
        public async void CreateCategory_ValidCategory_ReturnsOkResult_WithCreatedCategory()
        {
            // arrange
            var CategoryCreate = new CategoryDtoCreateRequest
            {
                Name = "man's clothes",
            };
            var categoryResponse = new CategoryDtoResponse
            {
                Id = 3,
                Name = "man's clothes",
                CreatedDate = DateTime.Now,
            };
            _categoryService.Setup(t => t.CreateCategoryAsync(CategoryCreate)).ReturnsAsync(categoryResponse);
            // act
            var response = await _categoryController.CreateCategory(CategoryCreate);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<CategoryDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal("man's clothes", Value.Name);

        }

        [Fact]
        public async void CreateCategory_WithShortCategoryNameLength_ReturnsBadRequestResult()
        {
            // arrange
            var CategoryCreate = new CategoryDtoCreateRequest
            {
                Name = "as",
            };
            _categoryService.Setup(t => t.CreateCategoryAsync(CategoryCreate)).ReturnsAsync(CategoryDtoFixture.CategoryList().First());
            _categoryController.ModelState.AddModelError("Name", "Your category name must be at least 3 characters long.");
            // act
            var response = await _categoryController.CreateCategory(CategoryCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("Your category name must be at least 3 characters long.", value["Name"] as string[]);
        }
        [Fact]
        public async void CreateCategory_WithCategoryNameLengthExceded_ReturnsBadRequestResult()
        {
            // arrange
            var CategoryCreate = new CategoryDtoCreateRequest
            {
                Name = "This is a fantastic product you should buy.This is a fantastic product you should buy.",
            };
            _categoryService.Setup(t => t.CreateCategoryAsync(CategoryCreate)).ReturnsAsync(CategoryDtoFixture.CategoryList().First());
            _categoryController.ModelState.AddModelError("Name", "the Name of category exceeds the maximum character limit of 20.");
            // act
            var response = await _categoryController.CreateCategory(CategoryCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("the Name of category exceeds the maximum character limit of 20.", value["Name"] as string[]);
        }
        [Fact]
        public async void CreateCategory_WithMissingCategoryName_ReturnsBadRequestResult()
        {
            // arrange
            var CategoryCreate = new CategoryDtoCreateRequest { };
            _categoryService.Setup(t => t.CreateCategoryAsync(CategoryCreate)).ReturnsAsync(CategoryDtoFixture.CategoryList().First());
            _categoryController.ModelState.AddModelError("Name", "Name is required");
            // act
            var response = await _categoryController.CreateCategory(CategoryCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("Name is required", value["Name"] as string[]);
        }
        //updateCategory
        [Fact]
        public async void UpdateCategory_ValidCategory_ReturnsOkResult_WithUpdatedCategory()
        {
            // arrange
            var categoryId = 1;
            var CategoryUpdate = new CategoryDtoUpdateRequest
            {
                Name = "man's clothes",
            };
            var categoryResponse = new CategoryDtoResponse
            {
                Id = 3,
                Name = "man's clothes",
                CreatedDate = DateTime.Now,
            };
            _categoryService.Setup(t => t.UpdateCategoryAsync(CategoryUpdate, categoryId)).ReturnsAsync(categoryResponse);
            // act
            var response = await _categoryController.UpdateCategory(CategoryUpdate, categoryId);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<CategoryDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal("man's clothes", Value.Name);

        }

        [Fact]
        public async void UpdateCategory_WithShortCategoryNameLength_ReturnsBadRequestResult()
        {
            // arrange
            var categoryId = 1;
            var CategoryUpdate = new CategoryDtoUpdateRequest
            {
                Name = "as",
            };
            _categoryService.Setup(t => t.UpdateCategoryAsync(CategoryUpdate, categoryId)).ReturnsAsync(CategoryDtoFixture.CategoryList().First());
            _categoryController.ModelState.AddModelError("Name", "Your category name must be at least 3 characters long.");
            // act
            var response = await _categoryController.UpdateCategory(CategoryUpdate, categoryId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("Your category name must be at least 3 characters long.", value["Name"] as string[]);
        }
        [Fact]
        public async void UpdateCategory_WithCategoryNameLengthExceded_ReturnsBadRequestResult()
        {
            // arrange
            var categoryId = 1;
            var CategoryUpdate = new CategoryDtoUpdateRequest
            {
                Name = "This is a fantastic product you should buy.This is a fantastic product you should buy.",
            };
            _categoryService.Setup(t => t.UpdateCategoryAsync(CategoryUpdate, categoryId)).ReturnsAsync(CategoryDtoFixture.CategoryList().First());
            _categoryController.ModelState.AddModelError("Name", "the Name of category exceeds the maximum character limit of 20.");
            // act
            var response = await _categoryController.UpdateCategory(CategoryUpdate, categoryId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("the Name of category exceeds the maximum character limit of 20.", value["Name"] as string[]);
        }
        [Fact]
        public async void UpdateCategory_WithMissingCategoryName_ReturnsBadRequestResult()
        {
            // arrange
            var categoryId = 1;
            var CategoryUpdate = new CategoryDtoUpdateRequest { };
            _categoryService.Setup(t => t.UpdateCategoryAsync(CategoryUpdate, categoryId)).ReturnsAsync(CategoryDtoFixture.CategoryList().First());
            _categoryController.ModelState.AddModelError("Name", "Name is required");
            // act
            var response = await _categoryController.UpdateCategory(CategoryUpdate, categoryId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("Name is required", value["Name"] as string[]);
        }
        [Fact]
        public async void UpdateCategory_shouldreturnCategoryIdNotfound_ReturnsNotFoundRequestResult()
        {
            // arrange
            var categoryId = 10;
            var CategoryUpdate = new CategoryDtoUpdateRequest 
            {
                Name = "man's clothes",
            };
            _categoryService.Setup(t => t.UpdateCategoryAsync(CategoryUpdate, categoryId)).ThrowsAsync(new KeyNotFoundException("category is not found"));
            
            // act
            var response = await _categoryController.UpdateCategory(CategoryUpdate, categoryId);
            // assert
            var notFoundrequestResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundrequestResult.StatusCode);
            Assert.Equal("category is not found", notFoundrequestResult.Value);
        }
    }
}