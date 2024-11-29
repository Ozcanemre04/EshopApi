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
    public class BasketProductRepositoryTest
    {
        private readonly Mock<AppDbContext> _context;

        public BasketProductRepositoryTest()
        {
            _context = new Mock<AppDbContext>();
        }


        [Fact]
        public async Task GetAllAsync_ShouldReturnBasketProductList()
        {
            // arrange 
            var userId = "bad206e0-3980-4746-893b-80afc748dfea";
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.GetAllAsync(userId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<List<BasketProduct>>(result);
            Assert.Equal(1, result.Count());
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnNullIfnotFound()
        {
            // arrange 
            var userId = "user2334";
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.GetAllAsync(userId);
            // asssert
            Assert.Empty(result);
            
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnOneBasketProductById()
        {
            // arrange 
            var basketProductId = 1;
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.GetOneAsync(basketProductId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<BasketProduct>(result);
            Assert.Equal(basketProducts.First().Id, result.Id);
            Assert.Equal(basketProducts.First().Quantity, result.Quantity);
            Assert.Equal(basketProducts.First().TotalPrice, result.TotalPrice);
            Assert.Equal(basketProducts.First().ProductId, result.ProductId);
            Assert.Equal(basketProducts.First().BasketId, result.BasketId);
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var basketProductId = 999;
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.GetOneAsync(basketProductId);
            // asssert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveBasketProduct()
        {
            // arrange 
            var basketProductId = 1;
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.DeleteAsync(basketProductId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(true, result);
            _context.Verify(x => x.Remove(It.Is<BasketProduct>(p => p.Id == basketProductId)));
            _context.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse()
        {
            // arrange 
            var basketProductId = 999;
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.DeleteAsync(basketProductId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(false, result);
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnBasketProductrWhenCreated()
        {
            // arrange             
            var basketProducts = BasketProductFixture.AllProductInBasket();
            var basketCreate = basketProducts.First();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.CreateAsync(basketCreate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<BasketProduct>(result);
            Assert.Equal(basketCreate.Id, result.Id);
            Assert.Equal(basketCreate.Quantity, result.Quantity);
            Assert.Equal(basketCreate.TotalPrice, result.TotalPrice);
            Assert.Equal(basketCreate.ProductId, result.ProductId);
            Assert.Equal(basketCreate.BasketId, result.BasketId);
            

        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnBasketProductWhenUpdated()
        {
            // arrange 
            var basketProducts = BasketProductFixture.AllProductInBasket();
            var basketUpdate = basketProducts.First();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.CreateAsync(basketUpdate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<BasketProduct>(result);
            Assert.Equal(basketUpdate.Id, result.Id);
            Assert.Equal(basketUpdate.Quantity, result.Quantity);
            Assert.Equal(basketUpdate.TotalPrice, result.TotalPrice);
            Assert.Equal(basketUpdate.ProductId, result.ProductId);
            Assert.Equal(basketUpdate.BasketId, result.BasketId);
        }
        [Fact]
        public async Task GetOneByUSerAndProductAsync_ShouldReturnOneBasketProductById()
        {
            // arrange 
            var basketProductId = 1;
            var productId = 1;
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.GetOneByBasketAndProductAsync(basketProductId,productId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<BasketProduct>(result);
            Assert.Equal(basketProducts.First().Id, result.Id);
            Assert.Equal(basketProducts.First().Quantity, result.Quantity);
            Assert.Equal(basketProducts.First().TotalPrice, result.TotalPrice);
            Assert.Equal(basketProducts.First().ProductId, result.ProductId);
            Assert.Equal(basketProducts.First().BasketId, result.BasketId);
        }
        [Fact]
        public async Task GetOneByUSerAndProductAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var basketProductId = 999;
            var productId = 999;
            var basketProducts = BasketProductFixture.AllProductInBasket();
            _context.Setup(p => p.BasketProducts).ReturnsDbSet(basketProducts);
            var basketProductRepository = new BasketProductRepository(_context.Object);
            // act
            var result = await basketProductRepository.GetOneByBasketAndProductAsync(basketProductId,productId);
            // asssert
            Assert.Null(result);
        }
    }
}