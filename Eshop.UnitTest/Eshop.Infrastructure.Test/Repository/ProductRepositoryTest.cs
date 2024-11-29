using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Configuration;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Eshop.Infrastructure.Repository;
using Eshop.Infrastructure.Test.Fixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Eshop.Infrastructure.Test.Repository
{
    public class ProductRepositoryTest
    {
        private readonly Mock<AppDbContext> _context;

        public ProductRepositoryTest()
        {
            _context = new Mock<AppDbContext>();
        }
        [Fact]
        public async Task Count_ShouldReturnCorrectCount()
        {
            // arrange 
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.Count();
            // asssert
            Assert.NotNull(result);
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnPagedProducts()
        {
            // arrange 
            int pageNumber = 1;
            int pageSize = 10;
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.GetAllAsync(pageNumber, pageSize);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<List<Product>>(result);
            Assert.Equal(ProductFixture.getAllProducts().Count(), result.Count());
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnOneProductById()
        {
            // arrange 
            var productId = 1;
            var product = ProductFixture.getAllProducts().First();
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.GetOneAsync(productId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(product.Name, result.Name);
            Assert.Equal(product.Id, result.Id);
            Assert.Equal(product.Price, result.Price);
            Assert.Equal(product.Description, result.Description);
            Assert.Equal(product.category.CategoryName, result.category.CategoryName);

        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var productId = 999;
            var product = ProductFixture.getAllProducts().First();
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.GetOneAsync(productId);
            // asssert
            Assert.Null(result);
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveProduct()
        {
            // arrange 
            var productId = 1;
            var product = ProductFixture.getAllProducts().First();
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.DeleteAsync(productId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(true, result);
            _context.Verify(x => x.Remove(It.Is<Product>(p => p.Id == productId)));
            _context.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse()
        {
            // arrange 
            var productId = 999;
            var product = ProductFixture.getAllProducts().First();
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.DeleteAsync(productId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(false, result);
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnProductWhenCreated()
        {
            // arrange             
            var productCreate = ProductFixture.getAllProducts().First();
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.CreateAsync(productCreate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productCreate.Name, result.Name);
            Assert.Equal(productCreate.Description, result.Description);
            Assert.Equal(productCreate.Price, result.Price);
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnProductWhenUpdated()
        {
            // arrange 
            var productUpdate = ProductFixture.getAllProducts().First();
            _context.Setup(p => p.Products).ReturnsDbSet(ProductFixture.getAllProducts());
            var productrepository = new ProductRepository(_context.Object);
            // act
            var result = await productrepository.CreateAsync(productUpdate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Product>(result);
            Assert.Equal(productUpdate.Name, result.Name);
            Assert.Equal(productUpdate.Description, result.Description);
            Assert.Equal(productUpdate.Price, result.Price);
        }
    }
}