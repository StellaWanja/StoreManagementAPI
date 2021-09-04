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
    public interface IProductDataStore
    {
        //add stores
        Task<Product> AddProduct(Product product);
        //display of a store
        Task<Product> GetProduct(string productId);
        //update store
        Task<bool> UpdateProduct(Product product);
        //delete store
        Task<bool> DeleteProduct(string productId);
    }
}