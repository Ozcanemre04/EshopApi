using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Product;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Product;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Mapper;
using Eshop.Application.Services;
using Eshop.Application.Test.Fixture;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Test.Fixture;
using Microsoft.Extensions.Logging;
using Moq;

namespace Eshop.Application.Test.Service
{
    public sealed class ProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly IMapper _mapper;
        private readonly ProductService _productService;
        private readonly ILogger<ProductService> _logger;

        public ProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
            _productService = new ProductService(_productRepositoryMock.Object, _mapper, _categoryRepositoryMock.Object,_logger);
        }

        [Fact]
        public async Task GetAllProductAsync_ShouldReturnPageProduct_()
        {
            //arrange
            int pageNumber = 0;
            int pageSize = 10;
            string categoryName = "Smartphone";
            string search = "";
            string order_type = "";
            bool asc = true;
            _productRepositoryMock.Setup(p => p.Count(categoryName,search)).ReturnsAsync(ProductFixture.getAllProducts().Count());
            _productRepositoryMock.Setup(p => p.GetAllComplexAsync(pageNumber, pageSize,categoryName,search,order_type,asc)).ReturnsAsync(ProductFixture.getAllProducts());
            //act
            var result = await _productService.GetAllProductAsync(pageNumber, pageSize,categoryName,search,order_type,asc);
            //assert
            Assert.NotNull(result);
            Assert.IsType<PageDto<ProductDtoResponse>>(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(ProductDtoFixture.getAllProducts().Count(), result.Data.Count());
            Assert.Equal(0, result.PageNumber);
            Assert.Equal(10, result.PageSize);
            Assert.IsType<ProductDtoResponse>(result.Data.First());
        }
        [Fact]
        public async Task GetAllProductAsync_ShouldReturnPageProduct_WhenSearchFilterIsUsed()
        {
            //arrange
            int pageNumber = 0;
            int pageSize = 10;
            string categoryName = "All";
            string search = "ip";
            string order_type = "";
            bool asc = true;
            var productlist = ProductFixture.getAllProducts().Where(x=>x.Name.ToLower().Contains(search));
            _productRepositoryMock.Setup(p => p.GetAllComplexAsync(pageNumber, pageSize,categoryName,search,order_type,asc)).ReturnsAsync(productlist);
            //act
            var result = await _productService.GetAllProductAsync(pageNumber, pageSize,categoryName,search,order_type,asc);
            //assert
            Assert.NotNull(result);
            Assert.IsType<PageDto<ProductDtoResponse>>(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(1, result.Data.Count());
            Assert.Equal(0, result.PageNumber);
            Assert.Equal(10, result.PageSize);
            Assert.IsType<ProductDtoResponse>(result.Data.First());
        }
        [Fact]
        public async Task GetAllProductAsync_ShouldReturnPageProduct_WhenPriceDESCSortIsUsed()
        {
            //arrange
            int pageNumber = 0;
            int pageSize = 10;
            string categoryName = "All";
            string search = "";
            string order_type = "price";
            bool asc = false;
            var productlist = ProductFixture.getAllProducts().OrderByDescending(x=>x.Price);
            _productRepositoryMock.Setup(p => p.GetAllComplexAsync(pageNumber, pageSize,categoryName,search,order_type,asc)).ReturnsAsync(productlist);
            //act
            var result = await _productService.GetAllProductAsync(pageNumber, pageSize,categoryName,search,order_type,asc);
            //assert
            Assert.NotNull(result);
            Assert.IsType<PageDto<ProductDtoResponse>>(result);
            Assert.NotEmpty(result.Data);
            Assert.Equal(3, result.Data.Count());
            Assert.Equal(235.43m, result.Data.First().Price);
            Assert.Equal(159.56m, result.Data.Last().Price);
            Assert.Equal(0, result.PageNumber);
            Assert.Equal(10, result.PageSize);
            Assert.IsType<ProductDtoResponse>(result.Data.First());
        }
        [Fact]
        public async Task GetAllProductAsync_ShouldReturnPageProduct_WhenCategoryIsBooks()
        {
            //arrange
            int pageNumber = 0;
            int pageSize = 10;
            string categoryName = "Books";
            string search = "";
            string order_type = "";
            bool asc = true;
            var productlist = ProductFixture.getAllProducts().Where(x=>x.category.CategoryName==categoryName);
            _productRepositoryMock.Setup(p => p.Count(categoryName,search)).ReturnsAsync(productlist.Count());
             _categoryRepositoryMock.Setup(p => p.GetOneByNameAsync(categoryName)).ReturnsAsync(CategoryFixture.CategoryList().Last());
            _productRepositoryMock.Setup(p => p.GetAllComplexAsync(pageNumber, pageSize,categoryName,search,order_type,asc)).ReturnsAsync(productlist);
            //act
            var result = await _productService.GetAllProductAsync(pageNumber, pageSize,categoryName,search,order_type,asc);
            //assert
            Assert.NotNull(result);
            Assert.IsType<PageDto<ProductDtoResponse>>(result);
            Assert.IsType<ProductDtoResponse>(result.Data.First());
            Assert.NotEmpty(result.Data);
            Assert.Equal(1, result.Data.Count());
            Assert.Equal(0, result.PageNumber);
            Assert.Equal(10, result.PageSize);
            
        }
        [Fact]
        public async Task GetAllProductAsync_ShouldReturnPageProduct_WhenCategoryIsNotFound()
        {
            //arrange
            int pageNumber = 0;
            int pageSize = 10;
            string categoryName = "hello";
            string search = "";
            string order_type = "";
            bool asc = true;
            var productlist = ProductFixture.getAllProducts().Where(x=>x.category.CategoryName==categoryName);
            _productRepositoryMock.Setup(p => p.Count(categoryName,search)).ReturnsAsync(productlist.Count());
            _categoryRepositoryMock.Setup(p => p.GetOneByNameAsync(categoryName)).ReturnsAsync(CategoryFixture.CategoryList().Last());
            _productRepositoryMock.Setup(p => p.GetAllComplexAsync(pageNumber, pageSize,categoryName,search,order_type,asc)).ReturnsAsync(productlist);
            //act
            var result = await _productService.GetAllProductAsync(pageNumber, pageSize,categoryName,search,order_type,asc);
            //assert
            Assert.NotNull(result);
            Assert.IsType<PageDto<ProductDtoResponse>>(result);
            Assert.Empty(result.Data);
            Assert.Equal(0, result.Data.Count());
            Assert.Equal(0, result.PageNumber);
            Assert.Equal(10, result.PageSize);
        }
        
        [Fact]
        public async Task GetOneProductAsync_ShouldReturnProductDtoResponse_whenProductExist()
        {
            //arrange
            long productId = 1;
            Product product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == productId)!;
            ProductDtoResponse productDtoResponse = ProductDtoFixture.getAllProducts().FirstOrDefault(x => x.Id == productId)!;
            _productRepositoryMock.Setup(p => p.AverageRatings(productId)).ReturnsAsync(3.5D);
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync(product);
            //act
            var result = await _productService.GetOneProductAsync(productId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<ProductDtoResponse>(result);
            Assert.Equal(productDtoResponse.Name, result.Name);
            Assert.Equal(productDtoResponse.Id, result.Id);
            Assert.Equal(productDtoResponse.Price, result.Price);
            Assert.Equal(productDtoResponse.Stock, result.Stock);
            Assert.Equal(productDtoResponse.Ratings, result.Ratings);
        }
        [Fact]
        public async Task GetOneProductAsync_ShouldThrowKeyNotFoundException_whenProductDoesNotExist()
        {
            //arrange
            long productId = 999;
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync((Product) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _productService.GetOneProductAsync(productId));
            Assert.Equal($"product with id: {productId} is not found", error.Result.Message);
            
           
        }
        //delete
        [Fact]
        public async Task DeleteProductAsync_ShouldReturnMessage_whenProductExist()
        {
            //arrange
            long productId = 1;
            _productRepositoryMock.Setup(p => p.DeleteAsync(productId)).ReturnsAsync(true);
            //act
            var result = await _productService.DeleteProductAsync(productId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("product is deleted",result);  
        }
        [Fact]
        public async Task DeleteProductAsync_ShouldThrowKeyNotFoundException_whenProductDoesNotExist()
        {
            //arrange
            long productId = 999;
            _productRepositoryMock.Setup(p => p.DeleteAsync(productId)).ReturnsAsync(false);
            //act assert
            var error= Assert.ThrowsAsync<KeyNotFoundException>(()=> _productService.DeleteProductAsync(productId));
            Assert.Equal("product is not found", error.Result.Message);   
        }
        //create
        [Fact]
        public async Task CreateProductAsync_ShouldReturnProductDtoResponse_WhenCategoryExists()
        {
            //arrange
            var cateogry = CategoryFixture.CategoryList().FirstOrDefault(x=>x.Id==1);
            var productDtoCreate = new ProductDtoCreateRequest
            {
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 159.56m,
                Stock = 10,
                CategoryId = 1,
                Image = "hello",

            };
            var productCreate = new Product
            {
                Id = 1,
                Name = "iphone",
                Description = "This is a fantastic product you should buy.",
                Price = 159.56m,
                Stock = 10,
                CategoryId = 1,
                Image = "hello",
                CreatedDate = DateTime.Now,
                category = cateogry
            };
           _categoryRepositoryMock.Setup(c=>c.GetOneAsync(cateogry.Id)).ReturnsAsync(cateogry);
            _productRepositoryMock.Setup(p => p.CreateAsync(productCreate)).ReturnsAsync(productCreate);
            //act
            var result = await _productService.CreateProductAsync(productDtoCreate);
            //assert
            Assert.NotNull(result);
            Assert.IsType<ProductDtoResponse>(result);
            Assert.Equal(ProductDtoFixture.getAllProducts().First().Stock, result.Stock);
            Assert.Equal(ProductDtoFixture.getAllProducts().First().Description, result.Description);
            Assert.Equal(ProductDtoFixture.getAllProducts().First().Price, result.Price);
            Assert.Equal(ProductDtoFixture.getAllProducts().First().CategoryId, result.CategoryId);
            Assert.Equal(ProductDtoFixture.getAllProducts().First().Image, result.Image);
            Assert.Equal(ProductDtoFixture.getAllProducts().First().Name, result.Name);
        }
        [Fact]
        public async Task CreateProductAsync_ShouldThrowKeyNotFoundException_WhenCategoryDoesNotExists()
        {
            //arrange
            var cateogry = 999;
            var productDtoCreate = new ProductDtoCreateRequest{};
           _categoryRepositoryMock.Setup(c=>c.GetOneAsync(cateogry)).ReturnsAsync((Category) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _productService.CreateProductAsync(productDtoCreate));
            Assert.Equal("category with this id doesn't exist", error.Result.Message);
            
        }
        //update
        [Fact]
        public async Task UpdateProductAsync_ShouldReturnProductDtoResponse_WhenProductExists()
        {
            //arrange
            var productId = ProductDtoFixture.getAllProducts().FirstOrDefault(x=>x.Id==1).Id;
            var product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id==1);
            var productDtoUpdate = new ProductDtoUpdateRequest
            {
                Name = "samsung",
                Description = "This is a fantastic product you should buy.",
                Price = 259.99m,
                Stock = 100,
                Image = "hello",

            };
            var productUpdate = new Product
            {
                Id = 1,
                Name = "samsung",
                Description = "This is a fantastic product you should buy.",
                Price = 259.99m,
                Stock = 100,
                CategoryId = 1,
                Image = "hello",
                CreatedDate = DateTime.Now,
                category = CategoryFixture.CategoryList().First(x=>x.Id==1),
            };
           
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync(product);
            _productRepositoryMock.Setup(p => p.UpdateAsync(productUpdate)).ReturnsAsync(productUpdate);
            //act
            var result = await _productService.UpdateProductAsync(productDtoUpdate,productId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<ProductDtoResponse>(result);
            Assert.Equal(productDtoUpdate.Stock, result.Stock);
            Assert.Equal(productDtoUpdate.Description, result.Description);
            Assert.Equal(productDtoUpdate.Price, result.Price);
            Assert.Equal(productDtoUpdate.Name, result.Name);
            Assert.Equal(productDtoUpdate.Image, result.Image);
            Assert.Equal(productUpdate.Id, result.Id);
            
        }
        [Fact]
        public async Task UpdateProductAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExists()
        {
            //arrange
            var productId = 999;
            var productDtoUpdate = new ProductDtoUpdateRequest{};
            _productRepositoryMock.Setup(c=>c.GetOneAsync(productId)).ReturnsAsync((Product) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _productService.UpdateProductAsync(productDtoUpdate,productId));
            Assert.Equal("product is not found", error.Result.Message);
            
        }
    }
}