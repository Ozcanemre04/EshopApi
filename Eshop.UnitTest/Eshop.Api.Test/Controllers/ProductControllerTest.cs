using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Eshop.Api.Controllers;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Dtos.Response.Commun;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Test.Fixture;

namespace Eshop.Api.Test.Controllers
{
    public class ProductControllerTest
    {
        private Mock<IProductService> _productService;
        private ProductController _productController;
        public ProductControllerTest()
        {
            _productService = new Mock<IProductService>();
            _productController = new ProductController(_productService.Object);
        }
        //getAllProduct
        [Fact]
        public async void GetAllProduct_ReturnsOKResult_WithAllProducts()
        {
            //arrange
            var categoryName = "All";
            var productList = ProductDtoFixture.getAllProducts().Where(x=>x.CategoryName == categoryName);
            _productService.Setup(t => t.GetAllProductAsync(1, 10, categoryName,"","",true)).ReturnsAsync(PageDtoFixture.pageDtoFixture());
            //act
            var response = await _productController.GetAllProducts(1, 10,categoryName,"","",true);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<PageDto<ProductDtoResponse>>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.IsType<ProductDtoResponse>(returnValue.Data.First());
            Assert.Equal(3, returnValue.Data.Count());
            Assert.Equal(1, returnValue.PageNumber);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(1, returnValue.TotalPages);
        }
        [Fact]
        public async void GetAllProduct_ReturnsOKResult_WithPriceSortDescUsed()
        {
            //arrange
            var categoryName = "All";
            bool asc = false;
            string sort_type = "price";
            var pageDto = PageDtoFixture.pageDtoFixture();
            pageDto.Data = pageDto.Data.OrderByDescending(x=>x.Price);
            _productService.Setup(t => t.GetAllProductAsync(1, 10, categoryName,"",sort_type,asc)).ReturnsAsync(pageDto);
            //act
            var response = await _productController.GetAllProducts(1, 10,categoryName,"",sort_type,asc);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<PageDto<ProductDtoResponse>>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.IsType<ProductDtoResponse>(returnValue.Data.First());
            Assert.Equal(3, returnValue.Data.Count());
             Assert.Equal(235.43m, returnValue.Data.First().Price);
            Assert.Equal(159.56m, returnValue.Data.Last().Price);
            Assert.Equal(1, returnValue.PageNumber);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(1, returnValue.TotalPages);
        }
        [Fact]
        public async void GetAllProduct_ReturnsOKResult_WithSearchFilterUsed()
        {
            //arrange
            var categoryName = "All";
            string search = "one";
            var pageDto = PageDtoFixture.pageDtoFixture();
             pageDto.Data =  pageDto.Data.Where(x=>x.Name.ToLower().Contains(search));
            _productService.Setup(t => t.GetAllProductAsync(1, 10, categoryName,search,"",true)).ReturnsAsync(pageDto);
            //act
            var response = await _productController.GetAllProducts(1, 10,categoryName,search,"",true);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<PageDto<ProductDtoResponse>>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.IsType<ProductDtoResponse>(returnValue.Data.First());
            Assert.Equal(1, returnValue.Data.Count());
            Assert.Equal(1, returnValue.PageNumber);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(1, returnValue.TotalPages);
        }

        [Fact]
        public async void GetAllProduct_ReturnsOKResult_WithProductsWithSmartphoneCategory()
        {
            //arrange
            var categoryName = "Smartphone";
            var productList = ProductDtoFixture.getAllProducts().Where(x=>x.CategoryName == categoryName);
            var updatedDataInPageDto = PageDtoFixture.pageDtoFixture();
            updatedDataInPageDto.Data = productList;
            updatedDataInPageDto.TotalRecords = productList.Count();
            updatedDataInPageDto.TotalPages = (int)Math.Ceiling(productList.Count()/(double)20);
            _productService.Setup(t => t.GetAllProductAsync(1, 20,categoryName,"","",true)).ReturnsAsync(updatedDataInPageDto);
            //act
            var response = await _productController.GetAllProducts(1, 20,categoryName,"","",true);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<PageDto<ProductDtoResponse>>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.IsType<ProductDtoResponse>(returnValue.Data.First());
            Assert.Equal(2, returnValue.Data.Count());
            Assert.Equal(2, returnValue.TotalRecords);
            Assert.Equal(1, returnValue.PageNumber);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(1, returnValue.TotalPages);
        }
        [Fact]
        public async void GetAllProduct_ReturnsOKResult_WithNoProducts()
        {
            //arrange
            var categoryName = "hello";
            var productList = ProductDtoFixture.getAllProducts().Where(x=>x.CategoryName == categoryName);
            var updatedDataInPageDto = PageDtoFixture.pageDtoFixture();
            updatedDataInPageDto.Data = productList;
            updatedDataInPageDto.TotalRecords = productList.Count(); 
            updatedDataInPageDto.TotalPages = (int)Math.Ceiling(productList.Count()/(double)20);
            _productService.Setup(t => t.GetAllProductAsync(1, 20,categoryName,"","",true)).ReturnsAsync(updatedDataInPageDto);
            //act
            var response = await _productController.GetAllProducts(1, 20,categoryName,"","",true);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<PageDto<ProductDtoResponse>>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(0, returnValue.Data.Count());
            Assert.Equal(0, returnValue.TotalRecords);
            Assert.Equal(1, returnValue.PageNumber);
            Assert.Equal(10, returnValue.PageSize);
            Assert.Equal(0, returnValue.TotalPages);
        }
        //getOneProduct
        [Fact]
        public async void GetOneProduct_ProductExist_ReturnsOKResult_WithProduct()
        {
            //arrange
            long productId = 1;
            _productService.Setup(t => t.GetOneProductAsync(productId)).ReturnsAsync(ProductDtoFixture.getAllProducts().FirstOrDefault(x => x.Id == productId));

            //act
            var response = await _productController.GetOneProduct(productId);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<ProductDtoResponse>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal(productId, returnValue.Id);
            Assert.Equal("iphone", returnValue.Name);
            Assert.Equal("Smartphone", returnValue.CategoryName);
            Assert.Equal("This is a fantastic product you should buy.", returnValue.Description);
            Assert.Equal("hello", returnValue.Image);
            Assert.Equal(10, returnValue.Stock);
            Assert.Equal(1, returnValue.CategoryId);
        }

        [Fact]
        public async void GetOneProduct_ProductNotFound_ReturnsNotFoundResult()
        {
            //arrange
            long productId = 1;
            _productService.Setup(t => t.GetOneProductAsync(productId)).ThrowsAsync(new KeyNotFoundException($"product with id: {productId} is not found"));

            //act
            var response = await _productController.GetOneProduct(productId);

            //assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal($"product with id: {productId} is not found", notFoundResult.Value);

        }

        //deleteProduct
        [Fact]
        public async void DeleteProduct_ProductExist_ReturnsOKResult()
        {
            //arrange
            long productId = 1;
            _productService.Setup(t => t.DeleteProductAsync(productId)).ReturnsAsync("product is deleted");

            //act
            var response = await _productController.DeleteProduct(productId);

            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal("product is deleted", returnValue);

        }
        [Fact]
        public async void DeleteProduct_ProductNotFound_ReturnsNotFoundResult()
        {
            //arrange
            long productId = 1;
            _productService.Setup(t => t.DeleteProductAsync(productId)).ThrowsAsync(new KeyNotFoundException("product is not found"));

            //act
            var response = await _productController.DeleteProduct(productId);

            //assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("product is not found", notFoundResult.Value);

        }
        //createProduct
        [Fact]
        public async void CreateProduct_ValidProduct_ReturnsOkResult_WithCreatedProduct()
        {
            // arrange
            var productCreate = new ProductDtoCreateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 159.56m,
                Stock = 10,
                CategoryId = 4,
                Image = "hello",

            };
            var productResponse = new ProductDtoResponse
            {
                Id = 3,
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 159.56m,
                Stock = 10,
                CategoryId = 4,
                Image = "hello",
                CreatedDate = DateTime.Now,
                CategoryName = "Smartphones",

            };
            _productService.Setup(t => t.CreateProductAsync(productCreate)).ReturnsAsync(productResponse);
            // act
            var response = await _productController.CreateProduct(productCreate);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<ProductDtoResponse>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal("iphone", returnValue.Name);
            Assert.Equal(159.56m, returnValue.Price);
            Assert.Equal("Smartphones", returnValue.CategoryName);
            Assert.Equal("This is a fantastic product you should buy.", returnValue.Description);
            Assert.Equal("hello", returnValue.Image);
            Assert.Equal(10, returnValue.Stock);
            Assert.Equal(4, returnValue.CategoryId);
        }

        [Fact]
        public async void CreateProduct_WithMissingProductNameAndDescriptipnAndIamge_ReturnsBadRequestResult()
        {
            // arrange
            var productCreate = new ProductDtoCreateRequest
            {
                Price = 159.56m,
                Stock = 10,
                CategoryId = 4,
            };

            _productService.Setup(t => t.CreateProductAsync(productCreate)).ReturnsAsync(ProductDtoFixture.getAllProducts().First());
            // act
            var response = await _productController.CreateProduct(productCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Name is required", value.FirstOrDefault(x => x.PropertyName == "Name").ErrorMessage);
            Assert.Equal("Description is required", value.FirstOrDefault(x => x.PropertyName == "Description").ErrorMessage);
            Assert.Equal("Product image is required.", value.FirstOrDefault(x => x.PropertyName == "Image").ErrorMessage);
            ;

        }
        [Fact]
        public async void CreateProduct_WithInvalidProductNameAndDescripionLengths_ReturnsBadRequestResult()
        {
            // arrange
            var productCreate = new ProductDtoCreateRequest
            {
                Name = "as",
                Description = "Th",
                Price = 159.56m,
                Stock = 10,
                CategoryId = 4,
                Image = "hello",
            };

            _productService.Setup(t => t.CreateProductAsync(productCreate)).ReturnsAsync(ProductDtoFixture.getAllProducts().First());
            // act
            var response = await _productController.CreateProduct(productCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("character length of product name must be between 3 and 200", value.FirstOrDefault(x => x.PropertyName == "Name").ErrorMessage);
            Assert.Equal("character length of description must be between 3 and 200", value.FirstOrDefault(x => x.PropertyName == "Description").ErrorMessage);
        }
        [Fact]
        public async void CreateProduct_WithInvalidPriceAndStockAndCategoryId_ReturnsBadRequestResult()
        {
            // arrange
            var productCreate = new ProductDtoCreateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 0,
                Stock = -1,
                CategoryId = -1,
                Image = "hello",
            };

            _productService.Setup(t => t.CreateProductAsync(productCreate)).ReturnsAsync(ProductDtoFixture.getAllProducts().First());
            // act
            var response = await _productController.CreateProduct(productCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Product price must be greater than zero.", value.FirstOrDefault(x => x.PropertyName == "Price").ErrorMessage);
            Assert.Equal("Product stock must be non-negative.", value.FirstOrDefault(x => x.PropertyName == "Stock").ErrorMessage);
            Assert.Equal("Product category id must be greater than 0", value.FirstOrDefault(x => x.PropertyName == "CategoryId").ErrorMessage);

        }
        [Fact]
        public async void CreateProduct_CategoryIdNotFound_ReturnsNotFoundResult()
        {
            // arrange
            var productCreate = new ProductDtoCreateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 100.99m,
                Stock = 100,
                CategoryId = 120,
                Image = "hello",
            };

            _productService.Setup(t => t.CreateProductAsync(productCreate)).ThrowsAsync(new KeyNotFoundException("category with this id doesn't exist"));
            // act
            var response = await _productController.CreateProduct(productCreate);
            // assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("category with this id doesn't exist", notFoundResult.Value);
        }
        //UpdateProduct
        [Fact]
        public async void UpdateProduct_ValidProduct_ReturnsOkResult_WithUpdatedProduct()
        {
            // arrange
            long productId = 1;
            var productUpdate = new ProductDtoUpdateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 159.56m,
                Stock = 10,
                Image = "hello",

            };
            var productResponse = new ProductDtoResponse
            {
                Id = 3,
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 159.56m,
                Stock = 10,
                CategoryId = 4,
                Image = "hello",
                CreatedDate = DateTime.Now,
                CategoryName = "Smartphones",

            };
            _productService.Setup(t => t.UpdateProductAsync(productUpdate, productId)).ReturnsAsync(productResponse);
            // act
            var response = await _productController.UpdateProduct(productUpdate, productId);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var returnValue = Assert.IsType<ProductDtoResponse>(okResult.Value);
            Assert.NotNull(returnValue);
            Assert.Equal("iphone", returnValue.Name);
            Assert.Equal(159.56m, returnValue.Price);
            Assert.Equal("Smartphones", returnValue.CategoryName);
            Assert.Equal("This is a fantastic product you should buy.", returnValue.Description);
            Assert.Equal("hello", returnValue.Image);
            Assert.Equal(10, returnValue.Stock);
            Assert.Equal(4, returnValue.CategoryId);
        }

        [Fact]
        public async void UpdateProduct_WithMissingProductNameAndDescriptipnAndImage_ReturnsBadRequestResult()
        {
            // arrange
            long productId = 1;
            var productUpdate = new ProductDtoUpdateRequest
            {
                Price = 159.56m,
                Stock = 10,

            };

            _productService.Setup(t => t.UpdateProductAsync(productUpdate, productId)).ReturnsAsync(ProductDtoFixture.getAllProducts().First());
            // act
            var response = await _productController.UpdateProduct(productUpdate, productId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Name is required", value.FirstOrDefault(x => x.PropertyName == "Name").ErrorMessage);
            Assert.Equal("Description is required", value.FirstOrDefault(x => x.PropertyName == "Description").ErrorMessage);
            Assert.Equal("Product image is required.", value.FirstOrDefault(x => x.PropertyName == "Image").ErrorMessage);
            ;

        }
        [Fact]
        public async void UpdateProduct_WithInvalidProductNameAndDescripionLengths_ReturnsBadRequestResult()
        {
            // arrange
            long productId = 1;
            var productUpdate = new ProductDtoUpdateRequest
            {
                Name = "as",
                Description = "Th",
                Price = 159.56m,
                Stock = 10,
                Image = "hello",
            };

            _productService.Setup(t => t.UpdateProductAsync(productUpdate, productId)).ReturnsAsync(ProductDtoFixture.getAllProducts().First());
            // act
            var response = await _productController.UpdateProduct(productUpdate, productId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("character length of product name must be between 3 and 40", value.FirstOrDefault(x => x.PropertyName == "Name").ErrorMessage);
            Assert.Equal("character length of description must be between 3 and 200", value.FirstOrDefault(x => x.PropertyName == "Description").ErrorMessage);
        }
        [Fact]
        public async void UpdateProduct_WithInvalidPriceAndStock_ReturnsBadRequestResult()
        {
            // arrange
            long productId = 1;
            var productUpdate = new ProductDtoUpdateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 0,
                Stock = -1,
                Image = "hello",
            };

            _productService.Setup(t => t.UpdateProductAsync(productUpdate, productId)).ReturnsAsync(ProductDtoFixture.getAllProducts().First());
            // act
            var response = await _productController.UpdateProduct(productUpdate, productId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Product price must be greater than zero.", value.FirstOrDefault(x => x.PropertyName == "Price").ErrorMessage);
            Assert.Equal("Product stock must be non-negative.", value.FirstOrDefault(x => x.PropertyName == "Stock").ErrorMessage);


        }
        [Fact]
        public async void UpdateProduct_IdNotFound_ReturnsNotFoundResult()
        {
            // arrange
            long productId = 9999;
            var productUpdate = new ProductDtoUpdateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 100.99m,
                Stock = 100,
                Image = "hello",
            };

            _productService.Setup(t => t.UpdateProductAsync(productUpdate, productId)).ThrowsAsync(new KeyNotFoundException("product is not found"));
            // act
            var response = await _productController.UpdateProduct(productUpdate, productId);
            // assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("product is not found", notFoundResult.Value);
        }

    }
}