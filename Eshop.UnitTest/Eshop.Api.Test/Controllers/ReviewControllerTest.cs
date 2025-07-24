using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Api.Controllers;
using Eshop.Application.Dtos.Request.Review;
using Eshop.Application.Dtos.Response.Commun;
using Eshop.Application.Dtos.Response.Review;
using Eshop.Application.Interfaces.Service;
using Eshop.Application.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Eshop.Api.Test.Controllers
{
    public class ReviewControllerTest
    {
        private Mock<IReviewService> _reviewServiceMock;
        private ReviewController _reviewControllerMock;

        public ReviewControllerTest()
        {
            _reviewServiceMock = new Mock<IReviewService>();
            _reviewControllerMock = new ReviewController(_reviewServiceMock.Object);

        }

        //allReviews
        [Fact]
        public async void GetAllReviews_ReturnsOKResult_WithReviews()
        {
            //arrange
            _reviewServiceMock.Setup(t => t.GetAllReviewsAsync(1)).ReturnsAsync(ReviewDtoFixture.ReviewList().Where(x=>x.ProductId==1).ToList());
            //act
            var response = await _reviewControllerMock.GetAllReviews(1);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var value = Assert.IsType<List<ReviewDtoResponse>>(okResult.Value);
            Assert.NotNull(value);
            Assert.Equal(1, value.Count);
        }


        //deleteReview
        [Fact]
        public async void DeleteReview_ReviewExist_ReturnsOKResult()
        {
            //arrange
            long reviewId = 1;
            var message = new MessageDto { Message = "review is deleted" };
            _reviewServiceMock.Setup(t => t.DeleteReviewAsync(ReviewDtoFixture.ReviewList().First().Id)).ReturnsAsync(message);
            //act
            var response = await _reviewControllerMock.DeleteReview(reviewId);
            //assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<MessageDto>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal(message.Message, Value.Message);

        }
        [Fact]
        public async void DeleteReview_UserNotAuthorizedException_ReturnsUnauthorizedrequestResult()
        {
            //arrange
            long reviewId = 1;
            _reviewServiceMock.Setup(t => t.DeleteReviewAsync(ReviewDtoFixture.ReviewList().First().Id)).ThrowsAsync(new UnauthorizedAccessException("Unauthorized"));
            //act
            var response = await _reviewControllerMock.DeleteReview(reviewId);
            //assert
            var unathorizedResult = Assert.IsType<UnauthorizedObjectResult>(response.Result);
            Assert.Equal(401, unathorizedResult.StatusCode);
            Assert.Equal("Unauthorized", unathorizedResult.Value);

        }
        [Fact]
        public async void DeleteReview_ReviewNotFound_ReturnsNotFoundResult()
        {
            //arrange
            long reviewId = 1;
            _reviewServiceMock.Setup(t => t.DeleteReviewAsync(reviewId)).ThrowsAsync(new KeyNotFoundException("review doesn't exist"));

            //act
            var response = await _reviewControllerMock.DeleteReview(reviewId);
            //assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("review doesn't exist", notFoundResult.Value);

        }
        //createReview
        [Fact]
        public async void CreateReview_ValidReview_ReturnsOkResult_WithCreatedReview()
        {
            // arrange
            var ReviewCreate = new ReviewDtoCreateRequest
            {
                Reviews = "CreateReview",
                Stars = 1,
                ProductId = 1
            };
            var categoryResponse = new ReviewDtoResponse
            {
                Reviews = "CreateReview",
                Stars = 1,
                ProductId = 1,
                UserId = "",
                Id = 1,
                CreatedDate = DateTime.Now,
            };
            _reviewServiceMock.Setup(t => t.CreateReviewAsync(ReviewCreate)).ReturnsAsync(categoryResponse);
            // act
            var response = await _reviewControllerMock.CreateReview(ReviewCreate);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<ReviewDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal("CreateReview", Value.Reviews);
            Assert.Equal(1, Value.Stars);
            Assert.Equal(1, Value.ProductId);
            Assert.Equal(1, Value.Id);

        }
        [Fact]
        public async void CreateReview_ProductNotFound_ReturnsNotFoundResult()
        {
            // arrange
            var ReviewCreate = new ReviewDtoCreateRequest
            {
                Reviews = "CreateReview",
                Stars = 1,
                ProductId = 1
            };

            _reviewServiceMock.Setup(t => t.CreateReviewAsync(ReviewCreate)).ThrowsAsync(new KeyNotFoundException("product doesn't exist"));
            // act
            var response = await _reviewControllerMock.CreateReview(ReviewCreate);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);
            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("product doesn't exist", NotFoundResult.Value);
        }

        [Fact]
        public async void CreateReview_WithMissingStarsAndReviews_ReturnsBadRequestResult()
        {
            // arrange
            var ReviewCreate = new ReviewDtoCreateRequest
            {
                ProductId = 1
            };
            _reviewServiceMock.Setup(t => t.CreateReviewAsync(ReviewCreate)).ReturnsAsync(ReviewDtoFixture.ReviewList().First());
            // act
            var response = await _reviewControllerMock.CreateReview(ReviewCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Stars are required", value.FirstOrDefault(x => x.PropertyName == "Stars").ErrorMessage);
            Assert.Equal("Review text is required.", value.FirstOrDefault(x => x.PropertyName == "Reviews").ErrorMessage);
        }
        [Fact]
        public async void CreateReview_WithInvalidReviewsAndStarsAndProductId_ReturnsBadRequestResult()
        {
            // arrange
            var ReviewCreate = new ReviewDtoCreateRequest
            {
                Reviews = "This",
                Stars = 6,
                ProductId = 0

            };
            _reviewServiceMock.Setup(t => t.CreateReviewAsync(ReviewCreate)).ReturnsAsync(ReviewDtoFixture.ReviewList().First());
            // act
            var response = await _reviewControllerMock.CreateReview(ReviewCreate);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Stars must be between 1 and 5.", value.FirstOrDefault(x => x.PropertyName == "Stars").ErrorMessage);
            Assert.Equal("character length of reviews must be between 10 and 200", value.FirstOrDefault(x => x.PropertyName == "Reviews").ErrorMessage);
            Assert.Equal("Product ID must be greater than 0.", value.FirstOrDefault(x => x.PropertyName == "ProductId").ErrorMessage);
        }

        //updateReview
        [Fact]
        public async void UpdateReview_ValidReview_ReturnsOkResult_WithUpdatedReview()
        {
            // arrange
            long reviewId = 1;
            var ReviewUpdate = new ReviewDtoUpdateRequest
            {
                Reviews = "This is a fantastic product you should buy.",
                Stars = 4,
            };
            var categoryResponse = new ReviewDtoResponse
            {
                Id = 3,
                Reviews = "This is a fantastic product you should buy",
                Stars = 4,
                ProductId = 1,
                UserId = "",
                CreatedDate = DateTime.Now,
            };
            _reviewServiceMock.Setup(t => t.UpdateReviewAsync(ReviewUpdate, reviewId)).ReturnsAsync(categoryResponse);
            // act
            var response = await _reviewControllerMock.UpdateReview(ReviewUpdate, reviewId);
            // assert
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var Value = Assert.IsType<ReviewDtoResponse>(okResult.Value);
            Assert.NotNull(Value);
            Assert.Equal("This is a fantastic product you should buy", Value.Reviews);
        }
        [Fact]
        public async void UpdateReview_ReviewnotFound_ReturnsNotfoundResult()
        {
            // arrange
            long reviewId = 1;
            var ReviewUpdate = new ReviewDtoUpdateRequest
            {
                Reviews = "This is a fantastic product you should buy.",
                Stars = 4,
            };
            _reviewServiceMock.Setup(t => t.UpdateReviewAsync(ReviewUpdate, reviewId)).ThrowsAsync(new KeyNotFoundException("review is not found"));
            // act
            var response = await _reviewControllerMock.UpdateReview(ReviewUpdate, reviewId);
            // assert
            var NotFoundResult = Assert.IsType<NotFoundObjectResult>(response.Result);

            Assert.Equal(404, NotFoundResult.StatusCode);
            Assert.Equal("review is not found", NotFoundResult.Value);
        }
        [Fact]
        public async void UpdateReview_UserUnauthorized_ReturnsUnauthorized()
        {
            // arrange
            long reviewId = 1;
            var ReviewUpdate = new ReviewDtoUpdateRequest
            {
                Reviews = "This is a fantastic product you should buy.",
                Stars = 4,
            };
            _reviewServiceMock.Setup(t => t.UpdateReviewAsync(ReviewUpdate, reviewId)).ThrowsAsync(new UnauthorizedAccessException("you can't edit other user review"));
            // act
            var response = await _reviewControllerMock.UpdateReview(ReviewUpdate, reviewId);
            // assert
            var UnauthorziedResult = Assert.IsType<UnauthorizedObjectResult>(response.Result);

            Assert.Equal(401, UnauthorziedResult.StatusCode);
            Assert.Equal("you can't edit other user review", UnauthorziedResult.Value);
        }

        [Fact]
        public async void UpdateReview_WithMissingStarsAndReviews_ReturnsBadRequestResult()
        {
            // arrange
            long reviewId = 1;
            var ReviewUpdate = new ReviewDtoUpdateRequest
            {

            };
            _reviewServiceMock.Setup(t => t.UpdateReviewAsync(ReviewUpdate, reviewId)).ReturnsAsync(ReviewDtoFixture.ReviewList().First());

            // act
            var response = await _reviewControllerMock.UpdateReview(ReviewUpdate, reviewId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("Stars are required", value.FirstOrDefault(x => x.PropertyName == "Stars").ErrorMessage);
            Assert.Equal("Review text is required.", value.FirstOrDefault(x => x.PropertyName == "Reviews").ErrorMessage);
        }
        [Fact]
        public async void UpdateReview_WithInvalidReviewsAndStars_ReturnsBadRequestResult()
        {
            // arrange
            long reviewId = 1;
            var ReviewUpdate = new ReviewDtoUpdateRequest
            {
                Reviews = "This",
                Stars = 0,
            };
            _reviewServiceMock.Setup(t => t.UpdateReviewAsync(ReviewUpdate, reviewId)).ReturnsAsync(ReviewDtoFixture.ReviewList().First());
            // act
            var response = await _reviewControllerMock.UpdateReview(ReviewUpdate, reviewId);
            // assert
            var badrequestResult = Assert.IsType<BadRequestObjectResult>(response.Result);
            Assert.Equal(400, badrequestResult.StatusCode);
            var value = Assert.IsType<List<FluentValidation.Results.ValidationFailure>>(badrequestResult.Value);
            Assert.Equal("character length of reviews must be between 10 and 200", value.FirstOrDefault(x => x.PropertyName == "Reviews").ErrorMessage);
            Assert.Equal("Stars must be between 1 and 5.", value.FirstOrDefault(x => x.PropertyName == "Stars").ErrorMessage);
        }

    }
}