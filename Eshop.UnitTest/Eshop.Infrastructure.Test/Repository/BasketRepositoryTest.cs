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
    public class BasketRepositoryTest
    {
        private readonly Mock<AppDbContext> _context;

        public BasketRepositoryTest()
        {
            _context = new Mock<AppDbContext>();
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnBasket()
        {
            // arrange 
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var baskets = BasketFixture.allBasket();
            _context.Setup(p => p.Baskets).ReturnsDbSet(baskets);
            var basketRepository = new BasketRepository(_context.Object);
            // act
            var result = await basketRepository.GetAllAsync(userId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Basket>(result);
            Assert.Equal(baskets.First().Id, result.Id);
            Assert.Equal(baskets.First().UserId, result.UserId);
            Assert.Equal(baskets.First().BasketProducts.Count(), result.BasketProducts.Count());
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnOneReviewById()
        {
            // arrange 
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var baskets = BasketFixture.allBasket();
            _context.Setup(p => p.Baskets).ReturnsDbSet(baskets);
            var basketRepository = new BasketRepository(_context.Object);
            // act
            var result = await basketRepository.GetOneAsync(userId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Basket>(result);
            Assert.Equal(baskets.First().UserId, result.UserId);
            Assert.Equal(baskets.First().Id, result.Id);
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var userId = "user234";
            var baskets = BasketFixture.allBasket();
            _context.Setup(p => p.Baskets).ReturnsDbSet(baskets);
            var basketRepository = new BasketRepository(_context.Object);
            // act
            var result = await basketRepository.GetOneAsync(userId);
            // asssert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnReviewWhenCreated()
        {
            // arrange             
            var baskets = BasketFixture.allBasket();
            var basketCreate = baskets.First();
            _context.Setup(p => p.Baskets).ReturnsDbSet(baskets);
            var basketRepository = new BasketRepository(_context.Object);
            // act
            var result = await basketRepository.CreateAsync(basketCreate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Basket>(result);
            Assert.Equal(basketCreate.UserId, result.UserId);
            Assert.Equal(basketCreate.Id, result.Id);


        }

    }
}