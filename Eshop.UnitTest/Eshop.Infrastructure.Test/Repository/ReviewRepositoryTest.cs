using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Eshop.Infrastructure.Repository;
using Eshop.Infrastructure.Test.Fixture;
using Moq;
using Moq.EntityFrameworkCore;

namespace Eshop.Infrastructure.Test.Repository
{
    public class ReviewRepositoryTest
    {
        private readonly Mock<AppDbContext> _context;

        public ReviewRepositoryTest()
        {
            _context = new Mock<AppDbContext>();
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnReviewList()
        {
            // arrange 
            var reviews = ReviewFixture.ReviewList();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.GetAllAsync();
            // asssert
            Assert.NotNull(result);
            Assert.IsType<List<Review>>(result);
            Assert.Equal(reviews.Count(), result.Count());
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnOneReviewById()
        {
            // arrange 
            var reviewId = 1;
            var reviews = ReviewFixture.ReviewList();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.GetOneAsync(reviewId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Review>(result);
            Assert.Equal(reviews.First().Stars, result.Stars);
            Assert.Equal(reviews.First().Id, result.Id);
            Assert.Equal(reviews.First().ProductId, result.ProductId);
            Assert.Equal(reviews.First().Reviews, result.Reviews);
            Assert.Equal(reviews.First().UserId, result.UserId);
            Assert.Equal(reviews.First().Product.Name, result.Product.Name);


        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var reviewId = 999;
            var reviews = ReviewFixture.ReviewList();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.GetOneAsync(reviewId);
            // asssert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveReview()
        {
            // arrange 
            var reviewId = 1;
            var reviews = ReviewFixture.ReviewList();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.DeleteAsync(reviewId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(true, result);
            _context.Verify(x => x.Remove(It.Is<Review>(p => p.Id == reviewId)));
            _context.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse()
        {
            // arrange 
            var reviewId = 999;
            var reviews = ReviewFixture.ReviewList();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.DeleteAsync(reviewId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(false, result);
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnReviewWhenCreated()
        {
            // arrange             
            var reviews = ReviewFixture.ReviewList();
            var reviewCreate = reviews.First();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.CreateAsync(reviewCreate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Review>(result);
            Assert.Equal(reviews.First().Stars, result.Stars);
            Assert.Equal(reviewCreate.Id, result.Id);
            Assert.Equal(reviewCreate.ProductId, result.ProductId);
            Assert.Equal(reviewCreate.Reviews, result.Reviews);
            Assert.Equal(reviewCreate.UserId, result.UserId);
            Assert.Equal(reviewCreate.Product.Name, result.Product.Name);

        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnReviewWhenUpdated()
        {
            // arrange 
            var reviews = ReviewFixture.ReviewList();
            var reviewUpdate = reviews.First();
            _context.Setup(p => p.Reviews).ReturnsDbSet(reviews);
            var reviewRepository = new ReviewRepository(_context.Object);
            // act
            var result = await reviewRepository.CreateAsync(reviewUpdate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Review>(result);
            Assert.Equal(reviewUpdate.Stars, result.Stars);
            Assert.Equal(reviewUpdate.Id, result.Id);
            Assert.Equal(reviewUpdate.ProductId, result.ProductId);
            Assert.Equal(reviewUpdate.Reviews, result.Reviews);
            Assert.Equal(reviewUpdate.UserId, result.UserId);
            Assert.Equal(reviewUpdate.Product.Name, result.Product.Name);

        }
    }
}