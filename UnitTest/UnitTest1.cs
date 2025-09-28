using FAS.DAL.Interfaces;
using FAS.DAL.Repositories;
using FAS.Model;

namespace UnitTest
{
    using Microsoft.EntityFrameworkCore;
    using System;
    using Xunit;

    public class UnitTest1
    {
        [Fact]
        public async Task TestCreateProduct()
        {
            // Arrange: Set up SQL Server DbContext
            var options = new DbContextOptionsBuilder<EcommerceDbContext>()
                .UseSqlServer()
                .Options;

            using var dbContext = new EcommerceDbContext(options);
            IProductDataAccess productDataAccess = new ProductDataAccess(dbContext);

            // Ensure the database is clean before the test
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var product = new Product
            {
                Name = "Test Product",
                Price = 9.99M
            };

            // Act: Create the product
            var result = await productDataAccess.CreateProductAsync(product);

            // Assert
            Assert.True(result);

            // Fetch the created product
            var createdProduct = await dbContext.Products.FirstOrDefaultAsync(p => p.Name == "Test Product");
            Assert.NotNull(createdProduct);
            Assert.Equal("Test Product", createdProduct.Name);
            Assert.Equal(9.99M, createdProduct.Price);

            // Test GetProductByIdAsync
            var fetchedProduct = await productDataAccess.GetProductByIdAsync(createdProduct.ProductId);
            Assert.NotNull(fetchedProduct);
            Assert.Equal(createdProduct.ProductId, fetchedProduct.ProductId);
            Assert.Equal("Test Product", fetchedProduct.Name);
            Assert.Equal(9.99M, fetchedProduct.Price);
        }
    }
}