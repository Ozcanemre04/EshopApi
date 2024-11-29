using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Api.Controllers;
using Eshop.Application.Dtos.Request.BasketProduct;
using Eshop.Application.Dtos.Response.BasketProduct;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Eshop.Api.Test.Controllers
{
    public class BasketControllerTest
    {
        private Mock<IBasketProductService> _BasketProductServiceMock;
        private BasketController _basketControllerMock;
        public BasketControllerTest()
        {
            _BasketProductServiceMock = new Mock<IBasketProductService>();
            _basketControllerMock = new BasketController(_BasketProductServiceMock.Object);
        }

        //allProductInBasket
        [Fact]
        public async void GetAllProductInBasketAsync_ReturnsOKResult_WithProducts()
        {
            //arrange
            _BasketProductServiceMock.Setup(t => t.GetAllBasketProductAsync()).ReturnsAsync(BasketDtoFixture.AllProductInBasket);
            //act
            var response = await _basketControllerMock.GetAllProductInBasketAsync();
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var value = Assert.IsType<BasketDtoResponse>(okResult.Value);
            Assert.NotNull(value);
            Assert.Equal(2, value.BasketProductDtoResponses.Count());
        }
        [Fact]
        public async void GetAllProductInBasketAsync_UserIDNotFound_ReturnsNotFoundResult()
        {
            //arrange
            _BasketProductServiceMock.Setup(t => t.GetAllBasketProductAsync()).ThrowsAsync(new KeyNotFoundException("basket doesn't exist"));
            //act
            var response = await _basketControllerMock.GetAllProductInBasketAsync();
            //assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.NotNull(NotFoundResult);
            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("basket doesn't exist", NotFoundResult.Value);
        }


        //deleteProductInBasket
        [Fact]
        public async void DeleteProductInBasketAsync_BasketExist_ReturnsOKResult()
        {
            //arrange
            long basketId = 1;
            _BasketProductServiceMock.Setup(t => t.DeleteBasketProductAsync(1)).ReturnsAsync("product is deleted");
            //act
            var response = await _basketControllerMock.DeleteProductInBasketAsync(basketId);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);

            var Value = Assert.IsType<string>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal("product is deleted", Value);
        }

        [Fact]
        public async void DeleteProductInBasketAsync_ProductNotFound_ReturnsNotFoundResult()
        {
            //arrange
            long basketId = 1;
            _BasketProductServiceMock.Setup(t => t.DeleteBasketProductAsync(basketId)).ThrowsAsync(new KeyNotFoundException("product is not found"));

            //act
            var response = await _basketControllerMock.DeleteProductInBasketAsync(basketId);
            //assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("product is not found", notFoundResult.Value);

        }
        //createReview
        [Fact]
        public async void CreateProductInBasketAsync_ValidBasket_ReturnsOkResult_WithCreatedBasket()
        {
            // arrange
            var BasketProductCreate = new BasketProductDtoCreateRequest
            {
                ProductId = 1
            };
            var BasketProductResponse = new BasketProductDtoResponse
            {
                Id = 2,
                CreatedDate = DateTime.Now,
                Quantity = 1,
                TotalPrice = 100,
                ProductId = 1,
                BasketId = 1
            };
            _BasketProductServiceMock.Setup(t => t.CreateBasketProductAsync(BasketProductCreate)).ReturnsAsync(BasketProductResponse);
            // act
            var response = await _basketControllerMock.CreateProductInBasketAsync(BasketProductCreate);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<BasketProductDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal(1, Value.Quantity);
            Assert.Equal(100, Value.TotalPrice);


        }
        [Fact]
        public async void CreateProductInBasketAsync_ProductNotFound_ReturnsNotFoundResult()
        {
            // arrange
            var BasketProductCreate = new BasketProductDtoCreateRequest
            {

                ProductId = 0
            };
            _BasketProductServiceMock.Setup(t => t.CreateBasketProductAsync(BasketProductCreate)).ThrowsAsync(new KeyNotFoundException("product doesn't exist"));
            // act
            var response = await _basketControllerMock.CreateProductInBasketAsync(BasketProductCreate);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("product doesn't exist", NotFoundResult.Value);
        }
        [Fact]
        public async void CreateProductInBasketAsync_BasketNotFound_ReturnsNotFoundResult()
        {
            // arrange
            var BasketProductCreate = new BasketProductDtoCreateRequest
            {

                ProductId = 2
            };
            _BasketProductServiceMock.Setup(t => t.CreateBasketProductAsync(BasketProductCreate)).ThrowsAsync(new KeyNotFoundException("basket doesn't exist"));
            // act
            var response = await _basketControllerMock.CreateProductInBasketAsync(BasketProductCreate);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("basket doesn't exist", NotFoundResult.Value);
        }

        [Fact]
        public async void CreateReview_WithMissingProductId_ReturnsBadRequestResult()
        {
            // arrange
            var BasketProductCreate = new BasketProductDtoCreateRequest
            {

            };
            _BasketProductServiceMock.Setup(t => t.CreateBasketProductAsync(BasketProductCreate)).ReturnsAsync(BasketProductDtoFixture.AllProductInBasket().First());
            _basketControllerMock.ModelState.AddModelError("ProductId", "ProductId is required");
            // act
            var response = await _basketControllerMock.CreateProductInBasketAsync(BasketProductCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("ProductId is required", value["ProductId"] as string[]);
        }
        [Fact]
        public async void CreateReview_WithInvalidProductId_ReturnsBadRequestResult()
        {
            // arrange
            var BasketProductCreate = new BasketProductDtoCreateRequest
            {
                ProductId = -12
            };
            _BasketProductServiceMock.Setup(t => t.CreateBasketProductAsync(BasketProductCreate)).ReturnsAsync(BasketProductDtoFixture.AllProductInBasket().First());
            _basketControllerMock.ModelState.AddModelError("ProductId", "ProductId for {0} must be between {1} and {2}.");
            // act
            var response = await _basketControllerMock.CreateProductInBasketAsync(BasketProductCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<SerializableError>(badrequestResult.Value);
            Assert.Contains("ProductId for {0} must be between {1} and {2}.", value["ProductId"] as string[]);
        }


        //increase Quantity
        [Fact]
        public async void IncreaseQuantityProductInBasketAsync_ShouldReturnsOkResult_WithIncreasedQuantityInBasket()
        {
            // arrange
            long basketId = 1;

            var BasketProductResponse = new BasketProductDtoResponse
            {
                Id = 2,
                CreatedDate = DateTime.Now,
                Quantity = 1,
                TotalPrice = 100,
                ProductId = 1,
                BasketId = 1
            };
            _BasketProductServiceMock.Setup(t => t.IncreaseQuantityAsync(basketId)).ReturnsAsync(BasketProductResponse);
            // act
            var response = await _basketControllerMock.IncreaseQuantityProductInBasketAsync(basketId);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<BasketProductDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal(2, Value.Id);

        }
        [Fact]
        public async void IncreaseQuantityProductInBasketAsync_ProductInBsaketNotFound_ReturnsNotfoundResult()
        {
            // arrange
            long basketId = 1;

            _BasketProductServiceMock.Setup(t => t.IncreaseQuantityAsync(basketId)).ThrowsAsync(new KeyNotFoundException("the Product in Basket is not found"));
            // act
            var response = await _basketControllerMock.IncreaseQuantityProductInBasketAsync(basketId);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);

            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("the Product in Basket is not found", NotFoundResult.Value);
        }
        [Fact]
        public async void IncreaseQuantityProductInBasketAsync_ProductNotFound_ReturnsNotfoundResult()
        {
            // arrange
            long basketId = 1;

            _BasketProductServiceMock.Setup(t => t.IncreaseQuantityAsync(basketId)).ThrowsAsync(new KeyNotFoundException("product doesn't exist"));
            // act
            var response = await _basketControllerMock.IncreaseQuantityProductInBasketAsync(basketId);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);

            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("product doesn't exist", NotFoundResult.Value);
        }
        [Fact]
        public async void IncreaseQuantityProductInBasketAsync_UserUnauthorized_ReturnsUnauthorized()
        {
            // arrange
            long basketId = 1;
            _BasketProductServiceMock.Setup(t => t.IncreaseQuantityAsync(basketId)).ThrowsAsync(new UnauthorizedAccessException("Unauthorized"));
            // act
            var response = await _basketControllerMock.IncreaseQuantityProductInBasketAsync(basketId);
            // assert
            var UnauthorziedResult = Assert.IsType<UnauthorizedObjectResult>(response.Result);
            Assert.Equal(401, UnauthorziedResult.StatusCode);
            Assert.Equal("Unauthorized", UnauthorziedResult.Value);
        }
        //decrease Quantity
        [Fact]
        public async void DecreaseQuantityProductInBasketAsync_ShouldReturnsOkResult_WithIncreasedQuantityInBasket()
        {
            // arrange
            long basketId = 1;

            var BasketProductResponse = new BasketProductDtoResponse
            {
                Id = 2,
                CreatedDate = DateTime.Now,
                Quantity = 1,
                TotalPrice = 100,
                ProductId = 1,
                BasketId = 1
            };
            _BasketProductServiceMock.Setup(t => t.DecreaseQuantityAsync(basketId)).ReturnsAsync(BasketProductResponse);
            // act
            var response = await _basketControllerMock.DecreaseQuantityProductInBasketAsync(basketId);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<BasketProductDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal(2, Value.Id);

        }
        [Fact]
        public async void DecreaseQuantityProductInBasketAsync_ProductInBsaketNotFound_ReturnsNotfoundResult()
        {
            // arrange
            long basketId = 1;
            _BasketProductServiceMock.Setup(t => t.DecreaseQuantityAsync(basketId)).ThrowsAsync(new KeyNotFoundException("the Product in Basket is not found"));
            // act
            var response = await _basketControllerMock.DecreaseQuantityProductInBasketAsync(basketId);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("the Product in Basket is not found", NotFoundResult.Value);
        }
        [Fact]
        public async void DecreaseQuantityProductInBasketAsync_ProductNotFound_ReturnsNotfoundResult()
        {
            // arrange
            long basketId = 1;
            _BasketProductServiceMock.Setup(t => t.DecreaseQuantityAsync(basketId)).ThrowsAsync(new KeyNotFoundException("product doesn't exist"));
            // act
            var response = await _basketControllerMock.DecreaseQuantityProductInBasketAsync(basketId);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("product doesn't exist", NotFoundResult.Value);
        }
        [Fact]
        public async void DecreaseQuantityProductInBasketAsync_UserUnauthorized_ReturnsUnauthorized()
        {
            // arrange
            long basketId = 1;
            _BasketProductServiceMock.Setup(t => t.DecreaseQuantityAsync(basketId)).ThrowsAsync(new UnauthorizedAccessException("Unauthorized"));
            // act
            var response = await _basketControllerMock.DecreaseQuantityProductInBasketAsync(basketId);
            // assert
            var UnauthorziedResult = Assert.IsType<UnauthorizedObjectResult>(response.Result);

            Assert.Equal(401, UnauthorziedResult.StatusCode);
            Assert.Equal("Unauthorized", UnauthorziedResult.Value);
        }
    }
}