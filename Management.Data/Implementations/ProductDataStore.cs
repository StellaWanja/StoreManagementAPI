using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Management.Models;

namespace Management.Data
{
    public class ProductDataStore : IProductDataStore
    {
        private readonly StoreDbContext _context;

        public ProductDataStore(StoreDbContext context)
        {
            _context = context;
        }

        //add store to database
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                await _context.AddAsync(product);
                var result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return product;
                }
                return product;
            }
            catch (Exception e)
            {             
                throw new Exception(e.Message);
            }       
        }

        //get details of a store
        public async Task<Product> GetProduct(string productId)
        {
            try
            {
                var product = await _context.Products
                    .FirstOrDefaultAsync(product => product.Id == productId);
                return product;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var findProduct = await _context.Products.FirstOrDefaultAsync(product => product.Id == product.Id);
            if (findProduct is null)
            {
                return false;
            }
            _context.Products.Update(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        //delete store
        public async Task<bool> DeleteProduct(string productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(product => product.Id == productId);
            if (product == null)
            {
                return false;
            }
            _context.Remove(product);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}