using FAS.DAL.Interfaces;
using FAS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAS.DAL.Repositories
{
    public class ProductDataAccess : IProductDataAccess
    {
        private readonly EcommerceDbContext dbContext;

        public ProductDataAccess(EcommerceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> CreateProductAsync(Product product)
        {
            try
            {
                await dbContext.Products.AddAsync(product);
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> GetProductByIdAsync(int productId)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public Task<bool> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
