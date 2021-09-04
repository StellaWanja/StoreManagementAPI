using Management.Models;
using Management.Data.DTOs.ProductDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Data.DTOs.Mappings
{

    // structure of the response 
    public class ProductMappings
    {
        public static AddProductResponse AddProductResponse(Product product)
        {
            return new AddProductResponse
            {
                Id = product.Id,
                StoreId = product.StoreId,
                ProductName = product.ProductName,
                Price = product.Price,
            };
        }

        public static GetProductResponse GetProductResponse(Product product)
        {
            return new GetProductResponse
            {
                Id = product.Id,
                StoreId = product.StoreId,
                ProductName = product.ProductName,
                Price = product.Price,
            };
        }
    }
}