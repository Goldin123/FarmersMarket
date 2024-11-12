using FarmersMarket.Database.Context;
using FarmersMarket.Database.Models;
using FarmersMarket.Logic.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmersMarket.Logic.Implementation
{
    public class ProductRepository: IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ProductRepository> _logger;

        public ProductRepository(ApplicationDbContext context,ILogger<ProductRepository> logger) 
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Product?> GetProductAsync(int id)
        {
            try
            {
                _logger.LogInformation($"Attempting to get a product with id: {id}.");
                return await _context.Products.FindAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}.");
                throw new Exception($"Internal server error: {ex.Message}.");
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try 
            {
                _logger.LogInformation($"Attempting to get all products.");
                return await _context.Products.ToListAsync();
            }
            catch (Exception ex) 
            {
                _logger.LogError($"Internal server error: {ex.Message}.");
                throw new Exception($"Internal server error: {ex.Message}.");
            }
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            try
            {
                _logger.LogInformation($"Attempting to get add a product {product.Name} , {product.Price}.");
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}.");
                throw new Exception($"Internal server error: {ex.Message}.");
            }
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!await ProductExistsAsync(product.Id))
                {
                    return false;
                }

                _logger.LogError($"Internal server error: {ex.Message}.");
                throw new Exception($"Internal server error: {ex.Message}.");
            }
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }

                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}.");
                throw new Exception($"Internal server error: {ex.Message}.");
            }
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            try
            {
                return await _context.Products.AnyAsync(e => e.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Internal server error: {ex.Message}.");
                throw new Exception($"Internal server error: {ex.Message}."); 
            }
        }
    }
}
