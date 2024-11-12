using FarmersMarket.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmersMarket.Logic.Interface
{
    public interface IProductRepository
    {
        Task<Product?> GetProductAsync(int id);
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> ProductExistsAsync(int id);
    }
}
