using FAS.BLL.BusinessInterfaces;
using FAS.DAL.Interfaces;
using FAS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FAS.BLL.BusinessService
{
    public class ProductService : IProductService
    {
        private readonly IProductDataAccess productDataAccess;
        public ProductService(IProductDataAccess productDataAccess)
        {
            this.productDataAccess = productDataAccess;
        }
        public async Task<bool> CreateProductAsync(Product product)
        {
            var p = await productDataAccess.GetProductByIdAsync(product.ProductId);
            if (p != null) return false;
            return await productDataAccess.CreateProductAsync(product);
        }

        public Task<bool> DeleteProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return productDataAccess.GetAllProductsAsync();
        }

        public Task<Product> GetProductByIdAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
