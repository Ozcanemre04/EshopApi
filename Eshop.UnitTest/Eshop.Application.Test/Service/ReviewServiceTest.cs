using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eshop.Application.Dtos.Request.Review;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Review;
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
    public class ReviewServiceTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly IMapper _mapper;
        private readonly ReviewService _reviewService;

        public ReviewServiceTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = config.CreateMapper();
            _reviewService = new ReviewService(_reviewRepositoryMock.Object, _mapper, _productRepositoryMock.Object, _currentUserServiceMock.Object);
        }

        [Fact]
        public async Task GetAllReviewsAsync_ShouldReturnListOfReviewDtoResponse()
        {
            //arrange
            _reviewRepositoryMock.Setup(p => p.GetAllAsync(1)).ReturnsAsync(ReviewFixture.ReviewList().Where(x=>x.ProductId==1).ToList());
            //act
            var result = await _reviewService.GetAllReviewsAsync(1);
            //assert
            Assert.NotNull(result);
            Assert.IsType<List<ReviewDtoResponse>>(result);
            Assert.NotEmpty(result);
            Assert.Equal(ReviewFixture.ReviewList().Where(x=>x.ProductId==1).Count(), result.Count());
        }

        //delete
        [Fact]
        public async Task DeleteReviewAsync_ShouldReturnMessage_whenReviewExist()
        {
            //arrange
            long reviewId = 1;
            var message = new MessageDto { Message = "review is deleted" };
            var review = ReviewFixture.ReviewList().FirstOrDefault(x => x.Id == reviewId);
            _currentUserServiceMock.Setup(u => u.UserId).Returns(review.UserId);
            _reviewRepositoryMock.Setup(p => p.GetOneAsync(reviewId)).ReturnsAsync(review);
            _reviewRepositoryMock.Setup(p => p.DeleteAsync(reviewId)).ReturnsAsync(true);
            //act
            var result = await _reviewService.DeleteReviewAsync(reviewId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<MessageDto>(result);
            Assert.Equal(message.Message, result.Message);
        }
        [Fact]
        public async Task DeleteReviewAsync_ShouldThrowKeyNotFoundException_whenReviewDoesNotExist()
        {
            //arrange
            long reviewId = 999;
            var review = ReviewFixture.ReviewList().FirstOrDefault(x => x.Id == reviewId);
            _reviewRepositoryMock.Setup(p => p.GetOneAsync(reviewId)).ReturnsAsync(review);
            _reviewRepositoryMock.Setup(p => p.DeleteAsync(reviewId)).ReturnsAsync(false);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(() => _reviewService.DeleteReviewAsync(reviewId));
            Assert.Equal("review is not found", error.Result.Message);
        }
        [Fact]
        public async Task DeleteReviewAsync_ShouldThrowUnauthorizedException_whenReviewUSerIdDoesNotMatchWithUserId()
        {
            //arrange
            long reviewId = 1;
            var userId = "user123";
            var review = ReviewFixture.ReviewList().FirstOrDefault(x => x.Id == reviewId);
            _currentUserServiceMock.Setup(u => u.UserId).Returns(userId);
            _reviewRepositoryMock.Setup(p => p.GetOneAsync(reviewId)).ReturnsAsync(review);
            _reviewRepositoryMock.Setup(p => p.DeleteAsync(reviewId)).ReturnsAsync(false);
            //act assert
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(() => _reviewService.DeleteReviewAsync(reviewId));
            Assert.Equal("Unauthorized", error.Result.Message);
        }
        //create
        [Fact]
        public async Task CreateReviewAsync_ShouldReturnReviewDtoResponse_WhenProductExists()
        {
            //arrange
            var product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == 1);
            var reviewDtoCreate = new ReviewDtoCreateRequest
            {
                Stars = 4,
                Reviews = "This is a fantastic product you should buy.This is a fantastic product you should buy.",
                ProductId = 1,
            };
            var reviewCreate = new Review
            {
                Id = 1,
                Stars = 4,
                Reviews = "This is a fantastic product you should buy.This is a fantastic product you should buy.",
                UserId = "bad206e0-3980-4746-893b-80afc748dfea",
                ProductId = 1,
                Product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == 1),
                CreatedDate = DateTime.Now,


            };
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _productRepositoryMock.Setup(c => c.GetOneAsync(product.Id)).ReturnsAsync(product);
            _reviewRepositoryMock.Setup(p => p.CreateAsync(reviewCreate)).ReturnsAsync(reviewCreate);
            //act
            var result = await _reviewService.CreateReviewAsync(reviewDtoCreate);
            //assert
            Assert.NotNull(result);
            Assert.IsType<ReviewDtoResponse>(result);
            Assert.Equal(ReviewDtoFixture.ReviewList().First().Reviews, result.Reviews);
            Assert.Equal(ReviewDtoFixture.ReviewList().First().ProductId, result.ProductId);
            Assert.Equal(ReviewDtoFixture.ReviewList().First().Stars, result.Stars);
            Assert.Equal(ReviewDtoFixture.ReviewList().First().UserId, result.UserId);
        }
        [Fact]
        public async Task CreateReviewAsync_ShouldThrowKeyNotFoundException_WhenProductDoesNotExists()
        {
            //arrange
            var productId = 999;
            var reviewDtoCreate = new ReviewDtoCreateRequest{ProductId=productId};
           _productRepositoryMock.Setup(c=>c.GetOneAsync(productId)).ReturnsAsync((Product) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _reviewService.CreateReviewAsync(reviewDtoCreate));
            Assert.Equal("product doesn't exist", error.Result.Message);
        }
        //update
        [Fact]
        public async Task UpdateReviewAsync_ShouldReturnReviewDtoResponse_WhenReviewExists()
        {
            //arrange
            var productId = ProductDtoFixture.getAllProducts().FirstOrDefault(x=>x.Id==1).Id;
            var review = ReviewFixture.ReviewList().FirstOrDefault(x=>x.Id==1);
            
            var reviewDtoUpdate = new ReviewDtoUpdateRequest
            {
                Stars = 5,
                Reviews = "This is a fantastic product you should buy.",

            };
            var reviewUpdate = new Review
            {
                Id = 1,
                Stars = 5,
                Reviews = "This is a fantastic product you should buy.",
                UserId = "bad206e0-3980-4746-893b-80afc748dfea",
                ProductId = 1,
                Product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == 1),
                CreatedDate = DateTime.Now,
            };
            _currentUserServiceMock.Setup(u => u.UserId).Returns("bad206e0-3980-4746-893b-80afc748dfea");
            _reviewRepositoryMock.Setup(p => p.GetOneAsync(productId)).ReturnsAsync(review);
            _reviewRepositoryMock.Setup(p => p.UpdateAsync(reviewUpdate)).ReturnsAsync(reviewUpdate);
            //act
            var result = await _reviewService.UpdateReviewAsync(reviewDtoUpdate,productId);
            //assert
            Assert.NotNull(result);
            Assert.IsType<ReviewDtoResponse>(result);
            Assert.Equal(reviewDtoUpdate.Reviews, result.Reviews);
            Assert.Equal(reviewUpdate.ProductId, result.ProductId);
            Assert.Equal(reviewDtoUpdate.Stars, result.Stars);
            Assert.Equal(reviewUpdate.UserId, result.UserId);
        }
        [Fact]
        public async Task UpdateReviewAsync_ShouldThrowKeyNotFoundException_WhenReviewDoesNotExists()
        {
            //arrange
            var reviewId = 999;
            var reviewDtoUpdate = new ReviewDtoUpdateRequest{};
            _reviewRepositoryMock.Setup(c=>c.GetOneAsync(reviewId)).ReturnsAsync((Review) null);
            //act assert
            var error = Assert.ThrowsAsync<KeyNotFoundException>(()=> _reviewService.UpdateReviewAsync(reviewDtoUpdate,reviewId));
            Assert.Equal("review is not found", error.Result.Message);

        }
        [Fact]
        public async Task UpdateReviewAsync_ShouldThrowUnauthorized_WhenReviewUserIsNotOwner()
        {
            //arrange
            var reviewId = 1;
            var reviewDtoUpdate = new ReviewDtoUpdateRequest{};
            var reviewUpdate = new Review
            {
                Id = 1,
                Stars = 4,
                Reviews = "This is a fantastic product you should buy.This is a fantastic product you should buy.",
                UserId = "bad206e0-3980-4746-893b-80afc748dfea",
                ProductId = 1,
                Product = ProductFixture.getAllProducts().FirstOrDefault(x => x.Id == 1),
                CreatedDate = DateTime.Now,
            };
             _reviewRepositoryMock.Setup(p => p.GetOneAsync(reviewId)).ReturnsAsync(reviewUpdate);
             _currentUserServiceMock.Setup(u => u.UserId).Returns("user123");
            _reviewRepositoryMock.Setup(p => p.UpdateAsync(reviewUpdate)).ReturnsAsync(reviewUpdate);
            
            //act assert
            var error = Assert.ThrowsAsync<UnauthorizedAccessException>(()=> _reviewService.UpdateReviewAsync(reviewDtoUpdate,reviewId));
            Assert.Equal("you can't edit other user review", error.Result.Message);

        }
    }
}