using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.BasketProduct;
using Eshop.Application.Dtos.Response.BasketProduct;
using Eshop.Application.Interfaces.Repository;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Mapper;
using Eshop.Application.Services;
using Eshop.Application.Test.Fixture;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Test.Fixture;
using Moq;

namespace Eshop.Application.Test.Service
{
    public class BasketProductServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IBasketProductRepository> _basketProductRepositoryMock;
        private readonly Mock<IBasketRepository> _basketRepositoryMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly IMapper _mapper;
        private readonly BasketProductService _basketProductService;

        public BasketProductServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _basketProductRepositoryMock = new Mock<IBasketProductRepository>();
            _basketRepositoryMock = new Mock<IBasketRepository>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
            _basketProductService = new BasketProductService(_basketProductRepositoryMock.Object, _mapper,
            _productRepositoryMock.Object, _currentUserServiceMock.Object, _basketRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllBasketProductAsync_ShouldBasketDtoResponse()
        {
            //arrange
            var basket = BasketFixture.allBasket().First();
            _currentUserServiceMock.Setup(x => x.UserId).Returns(basket.UserId);
            _basketRepositoryMock.Setup(p => p.GetAllAsync(basket.UserId)).ReturnsAsync(basket);
            //act
            var result = await _basketProductService.GetAllBasketProductAsync();
            //assert
            Assert.NotNull(result);
            Assert.IsType<BasketDtoResponse>(result);
            Assert.IsType<List<BasketProductDtoResponse>>(result.BasketProductDtoResponses);
            Assert.Equal(BasketProductDtoFixture.AllProductInBasket().Count(), result.BasketProductDtoResponses.Count());
        }

        //delete
        [Fact]
        public async Task DeleteBasketProductAsync_ShouldReturnSuccessMessage_whenBasketExist()
        {
            //arrange
            long basketId = 1;
            var basket = BasketProductFixture.AllProductInBasket().FirstOrDefault(x => x.Id == basketId);
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(basketId)).ReturnsAsync(basket);
            _basketProductRepositoryMock.Setup(p => p.DeleteAsync(basketId)).ReturnsAsync(true);
            //act
            var result = await _basketProductService.DeleteBasketProductAsync(basketId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("product is deleted", result);
            _basketProductRepositoryMock.Verify(p => p.GetOneAsync(basketId), Times.Once);
            _basketProductRepositoryMock.Verify(p => p.DeleteAsync(basketId), Times.Once);
        }
        [Fact]
        public async Task DeleteBasketProductAsync_ShouldThrowKeyNotFoundException_whenBasketDoesNotExist()
        {
            //arrange
            long basketId = 999;
            var basket = BasketProductFixture.AllProductInBasket().FirstOrDefault(x => x.Id == basketId);
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(basketId)).ReturnsAsync(basket);
            _basketProductRepositoryMock.Setup(p => p.DeleteAsync(basketId)).ReturnsAsync(false);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(() => _basketProductService.DeleteBasketProductAsync(basketId));
            Assert.Equal("product is not found", error.Result.Message);
        }
        [Fact]
        public async Task DeleteBasketProductAsync_ShouldThrowUnauthorizedException_whenBasketUSerIdDoesNotMatchWithUserId()
        {
            //arrange
            long basketId = 1;
            var userId = "user123";
            var basket = BasketProductFixture.AllProductInBasket().FirstOrDefault(x => x.Id == basketId);
            _currentUserServiceMock.Setup(u => u.UserId).Returns(userId);
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(basketId)).ReturnsAsync(basket);
            _basketProductRepositoryMock.Setup(p => p.DeleteAsync(basketId)).ReturnsAsync(false);
            //act assert
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(() => _basketProductService.DeleteBasketProductAsync(basketId));
            Assert.Equal("unauthorized", error.Result.Message);
        }
        //create
        [Fact]
        public async Task CreateBasketProductAsync_ShouldReturnReviewDtoResponse_WhenProductExists()
        {
            //arrange
            var product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == 2);
            var basket = BasketFixture.allBasket().FirstOrDefault(x => x.Id == 1);
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var BasketProductDtoCreate = new BasketProductDtoCreateRequest
            {
                ProductId = 2,
            };
            var basketCreate = new BasketProduct
            {
                Id = 2,
                CreatedDate = DateTime.Now,
                Quantity = 1,
                TotalPrice = 235.43m,
                ProductId = 2,
                BasketId = 1,
                Basket = new Basket
                {
                    UserId = "bad206e0-3980-4746-893b-80afc748dfea"
                },
            };
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _productRepositoryMock.Setup(c => c.GetOneAsync(product.Id)).ReturnsAsync(product);
            _basketRepositoryMock.Setup(c => c.GetOneAsync(userId)).ReturnsAsync(BasketFixture.allBasket().First());
            _basketProductRepositoryMock.Setup(p => p.GetOneByBasketAndProductAsync(2, 2)).ReturnsAsync(basketCreate);
            _basketProductRepositoryMock.Setup(p => p.CreateAsync(basketCreate)).ReturnsAsync(basketCreate);
            //act
            var result = await _basketProductService.CreateBasketProductAsync(BasketProductDtoCreate);
            //assert
            Assert.NotNull(result);
            Assert.IsType<BasketProductDtoResponse>(result);
            Assert.Equal(basketCreate.Quantity, result.Quantity);
            Assert.Equal(basketCreate.TotalPrice, result.TotalPrice);
            Assert.Equal(BasketProductDtoCreate.ProductId, result.ProductId);
            Assert.Equal(basketCreate.BasketId, result.BasketId);
        }
        [Fact]
        public async Task CreateBasketProductAsync_ShouldReturnReviewDtoResponseAndIncreaseQuantity_WhenProductExists()
        {
            //arrange
            var product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == 1);
            var basket = BasketFixture.allBasket().FirstOrDefault(x => x.Id == 1);
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var BasketProductDtoCreate = new BasketProductDtoCreateRequest
            {
                ProductId = 1,
            };
            var basketCreate = new BasketProduct
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                Quantity = 1,
                TotalPrice = 159.56m,
                ProductId = 1,
                BasketId = 1,
                Basket = new Basket
                {
                    UserId = "bad206e0-3980-4746-893b-80afc748dfea"
                },
            };
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _productRepositoryMock.Setup(c => c.GetOneAsync(product.Id)).ReturnsAsync(product);
            _basketRepositoryMock.Setup(c => c.GetOneAsync(userId)).ReturnsAsync(BasketFixture.allBasket().First());
            _basketProductRepositoryMock.Setup(p => p.GetOneByBasketAndProductAsync(basket.Id, product.Id)).ReturnsAsync(basketCreate);
            _basketProductRepositoryMock.Setup(p => p.UpdateAsync(basketCreate)).ReturnsAsync(basketCreate);
            //act
            var result = await _basketProductService.CreateBasketProductAsync(BasketProductDtoCreate);
            //assert
            Assert.NotNull(result);
            Assert.IsType<BasketProductDtoResponse>(result);

            Assert.Equal(2, result.Quantity);
            Assert.Equal(159.56m * 2, result.TotalPrice);
            Assert.Equal(1, result.ProductId);
            Assert.Equal(1, result.BasketId);
        }
        [Fact]
        public async Task CreateBasketProductAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExists()
        {
            //arrange
            var productId = 999;
            var BasketProductDtoCreate = new BasketProductDtoCreateRequest
            {
                ProductId = productId,
            };
            
            _productRepositoryMock.Setup(c => c.GetOneAsync(productId)).ReturnsAsync((Product)null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(() => _basketProductService.CreateBasketProductAsync(BasketProductDtoCreate));
            Assert.Equal("product doesn't exist", error.Result.Message);
        }
        [Fact]
        public async Task CreateBasketProductAsync_ShouldThrowKeyNotFoundException_WhenBasketDoesNotExists()
        {
            //arrange
            var productId = 1;
            var product = ProductFixture.getAllProducts().First();
            var userId = "user123";
            var BasketProductDtoCreate = new BasketProductDtoCreateRequest
            {
                ProductId = productId,
            };
            _productRepositoryMock.Setup(c => c.GetOneAsync(productId)).ReturnsAsync(product);
            _currentUserServiceMock.Setup(u => u.UserId).Returns("user123");
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(() => _basketProductService.CreateBasketProductAsync(BasketProductDtoCreate));
            Assert.Equal("basket doesn't exist", error.Result.Message);
        }
        //update increase
        [Fact]
        public async Task IncreaseQuantityAsync_ShouldReturnBasketProductDtoResponse_WhenBasketProductExists()
        {
            //arrange
            var product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id==1);
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);
            var BasketProductId = 1;
            basketProduct.Quantity = 1;
            basketProduct.TotalPrice = 159.56m;
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(product.Id)).ReturnsAsync(product);
            _basketProductRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<BasketProduct>())).ReturnsAsync((BasketProduct basketProduct)=>basketProduct);
            //act
            var result = await _basketProductService.IncreaseQuantityAsync(BasketProductId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<BasketProductDtoResponse>(result);
            Assert.Equal(2, result.Quantity);
            Assert.Equal(159.56m*2, result.TotalPrice);
           
        }
        [Fact]
        public async Task IncreaseQuantityAsync_ShouldThrowKeyNotFoundException_WhenBasketProductDoesNotExists()
        {
            //arrange
            var BasketProductId = 999;
            _basketProductRepositoryMock.Setup(c=>c.GetOneAsync(BasketProductId)).ReturnsAsync((BasketProduct) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _basketProductService.IncreaseQuantityAsync(BasketProductId));
            Assert.Equal("the Product in Basket is not found", error.Result.Message);

        }
        [Fact]
        public async Task IncreaseQuantityAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExists()
        {
            //arrange
            var BasketProductId = 1;
            var productId = 999;
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);

            _basketProductRepositoryMock.Setup(c=>c.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync((Product) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _basketProductService.IncreaseQuantityAsync(BasketProductId));
            Assert.Equal("product doesn't exist", error.Result.Message);

        }
        [Fact]
        public async Task IncreaseQuantityAsync_ShouldThrowUnauthorized_WhenBasketProductUserIsNotOwner()
        {
            //arrange
            var BasketProductId = 1;
            var productId = 1;
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);
            var product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id==1);
            basketProduct.Basket.UserId = "bad206e0-3980-4746-893b-80afc748dfea";
             _currentUserServiceMock.Setup(u => u.UserId).Returns("user123");
            _basketProductRepositoryMock.Setup(c=>c.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync(product);
            //act assert
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(()=> _basketProductService.IncreaseQuantityAsync(BasketProductId));
            Assert.Equal("Unauthorized", error.Result.Message);
        }
        //update decrease
        [Fact]
        public async Task DecreaseQuantityAsync_ShouldReturnBasketProductDtoResponse_WhenBasketProductExistsAndQuantityisGreaterThanZero()
        {
            //arrange
            var product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id==1);
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);
            var BasketProductId = 1;
            basketProduct.Quantity = 2;
            basketProduct.TotalPrice = 159.56m *2;
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(product.Id)).ReturnsAsync(product);
            _basketProductRepositoryMock.Setup(p => p.UpdateAsync(It.IsAny<BasketProduct>())).ReturnsAsync((BasketProduct basketProduct)=>basketProduct);
            //act
            var result = await _basketProductService.DecreaseQuantityAsync(BasketProductId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<BasketProductDtoResponse>(result);
            Assert.Equal(1, result.Quantity);
            Assert.Equal(159.56m, result.TotalPrice);
        }
        [Fact]
        public async Task DecreaseQuantityAsync_ShouldReturnBasketProductDtoResponse_WhenBasketProductExistsAndQuantityEqualZero()
        {
            //arrange
            var product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id==1);
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);
            var BasketProductId = 1;
            basketProduct.Quantity = 1;
            basketProduct.TotalPrice = 159.56m;
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(product.Id)).ReturnsAsync(product);
            _basketProductRepositoryMock.Setup(p => p.DeleteAsync(BasketProductId)).ReturnsAsync(true);
            //act
            var result = await _basketProductService.DecreaseQuantityAsync(BasketProductId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<BasketProductDtoResponse>(result);
            Assert.Equal(0, result.Quantity);
            Assert.Equal(0, result.TotalPrice);
            _basketProductRepositoryMock.Verify(c => c.DeleteAsync(BasketProductId), Times.Once);
            //arrange act assert for the exception
            _basketProductRepositoryMock.Setup(p => p.GetOneAsync(BasketProductId)).ReturnsAsync((BasketProduct)null);
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _basketProductService.DecreaseQuantityAsync(BasketProductId));
            Assert.Equal("the Product in Basket is not found",error.Result.Message);
        }
        [Fact]
        public async Task DecreaseQuantityAsync_ShouldThrowKeyNotFoundException_WhenBasketProductDoesNotExists()
        {
            //arrange
            var BasketProductId = 999;
            _basketProductRepositoryMock.Setup(c=>c.GetOneAsync(BasketProductId)).ReturnsAsync((BasketProduct) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _basketProductService.DecreaseQuantityAsync(BasketProductId));
            Assert.Equal("the Product in Basket is not found", error.Result.Message);

        }
        [Fact]
        public async Task DecreaseQuantityAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExists()
        {
            //arrange
            var BasketProductId = 1;
            var productId = 999;
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);

            _basketProductRepositoryMock.Setup(c=>c.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync((Product) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _basketProductService.DecreaseQuantityAsync(BasketProductId));
            Assert.Equal("product doesn't exist", error.Result.Message);

        }
        [Fact]
        public async Task DecreaseQuantityAsync_ShouldThrowUnauthorized_WhenBasketProductUserIsNotOwner()
        {
            //arrange
            var BasketProductId = 1;
            var productId = 1;
            var basketProduct = BasketProductFixture.AllProductInBasket().FirstOrDefault(x=>x.Id==1);
            var product = ProductFixture.getAllProducts().FirstOrDefault(x=>x.Id==1);
            basketProduct.Basket.UserId = "bad206e0-3980-4746-893b-80afc748dfea";
             _currentUserServiceMock.Setup(u => u.UserId).Returns("user123");
            _basketProductRepositoryMock.Setup(c=>c.GetOneAsync(BasketProductId)).ReturnsAsync(basketProduct);
            _productRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync(product);
            //act assert
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(()=> _basketProductService.DecreaseQuantityAsync(BasketProductId));
            Assert.Equal("Unauthorized", error.Result.Message);
        }
        
    }
}