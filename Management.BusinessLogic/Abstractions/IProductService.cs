using Management.Models;
using Management.Data;
using Management.Data.DTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Management.Data.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;

namespace Management.BusinessLogic
{
    public interface IProductService
    {
        Task<Product> CreateProduct(string storeId, string productName, int price);
        Task<Product> DisplayProduct(string productId);
        Task<bool> UpdateProductUsingPut(PutProductRequest productDTO, string storeId, string productId);
        Task<bool> UpdateProductUsingPatch(PatchProductRequest productDTO, string storeId, string productId);
        Task<bool> RemoveProduct(string productId);
    }
}