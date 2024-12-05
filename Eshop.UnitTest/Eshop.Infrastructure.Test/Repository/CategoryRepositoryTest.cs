using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eshop.Domain.Entities;
using Eshop.Infrastructure.Data;
using Eshop.Infrastructure.Repository;
using Eshop.Infrastructure.Test.Fixture;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace Eshop.Infrastructure.Test.Repository
{
    public class CategoryRepositoryTest
    {
        private readonly Mock<AppDbContext> _context;

        public CategoryRepositoryTest()
        {
            _context = new Mock<AppDbContext>();
        }
        

        [Fact]
        public async Task GetAllAsync_ShouldReturnCategoriesList()
        {
            // arrange 
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.GetAllAsync();
            // asssert
            Assert.NotNull(result);
            Assert.IsType<List<Category>>(result);
            Assert.Equal(categories.Count(), result.Count());
        }
        [Fact]
        public async Task GetOneAsync_ShouldReturnOneCategoryById()
        {
            // arrange 
            var categoryId = 1;
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.GetOneAsync(categoryId);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categories.First().CategoryName, result.CategoryName);
            Assert.Equal(categories.First().Id, result.Id);
        }
        
        [Fact]
        public async Task GetOneAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var categoryId = 999;
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.GetOneAsync(categoryId);
            // asssert
            Assert.Null(result);
        }
        [Fact]
        public async Task GetOneByNameAsync_ShouldReturnOneCategoryByName()
        {
            // arrange 
            var categoryName = "Smartphone";
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.GetOneByNameAsync(categoryName);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categories.First().CategoryName, result.CategoryName);
            Assert.Equal(categories.First().Id, result.Id);
        }
        [Fact]
        public async Task GetOneByNameAsync_ShouldReturnNullIfNotFound()
        {
            // arrange 
            var categoryName = "nothing";
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.GetOneByNameAsync(categoryName);
            // asssert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveCategory()
        {
            // arrange 
            var categoryId = 1;
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.DeleteAsync(categoryId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(true, result);
            _context.Verify(x => x.Remove(It.Is<Category>(p => p.Id == categoryId)));
            _context.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldReturnFalse()
        {
            // arrange 
            var categoryId = 999;
            var categories = CategoryFixture.CategoryList();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.DeleteAsync(categoryId);
            // asssert
            Assert.NotNull(result);
            Assert.Equal(false, result);
        }
        [Fact]
        public async Task CreateAsync_ShouldReturnCategoryWhenCreated()
        {
            // arrange             
            var categories = CategoryFixture.CategoryList();
            var categoryCreate = categories.First();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.CreateAsync(categoryCreate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryCreate.CategoryName, result.CategoryName);
            Assert.Equal(categoryCreate.Id, result.Id);
            
        }
        [Fact]
        public async Task UpdateAsync_ShouldReturnCategoryWhenUpdated()
        {
            // arrange 
            var categories = CategoryFixture.CategoryList();
            var categoryUpdate = categories.First();
            _context.Setup(p => p.Categories).ReturnsDbSet(categories);
            var categoryrepository = new CategoryRepository(_context.Object);
            // act
            var result = await categoryrepository.CreateAsync(categoryUpdate);
            // asssert
            Assert.NotNull(result);
            Assert.IsType<Category>(result);
            Assert.Equal(categoryUpdate.CategoryName, result.CategoryName);
            Assert.Equal(categoryUpdate.Id, result.Id);
           
        }
    }
}