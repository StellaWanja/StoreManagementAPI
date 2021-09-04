using Management.Models;
using Management.Data;
using Management.Data.DTOs;
using Management.Data.DTOs.ProductDTOs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Management.BusinessLogic
{
    public class ProductService : IProductService
    {
        //actions to collect data from store
        private readonly IProductDataStore _dataStore;

        public ProductService(IProductDataStore dataStore)
        {
            _dataStore = dataStore ?? throw new ArgumentNullException(nameof(dataStore));
        }

        //create store method
        public async Task<Product> CreateProduct(string storeId, string productName, int price)
        {
            Product product = new Product
            {
                StoreId = storeId,
                ProductName = productName,
                Price = price
            };
            var result = await _dataStore.AddProduct(product);            
            if(result != null)
            {
                return product;
            }
            throw new TimeoutException("Unable to create product instance at this time"); 
        }

        // Get specific product
        public async Task<Product> DisplayProduct(string productId)
        {
            Product product = await _dataStore.GetProduct(productId);
            if (product is null)
            {
                throw new ArgumentNullException("Resource does not exist");
            }
            return product;
        }

        //update an entire resource/store
        public async Task<bool> UpdateProductUsingPut(PutProductRequest productDTO, string storeId, string productId)
        {
            try
            {
                //get the store details
                var product = await DisplayProduct(productId);
                //if product is null, return false
                if (product == null)
                {
                    return false;
                }
                //if user id doesnt match, throw exception
                if (product.StoreId != storeId)
                {
                    throw new UnauthorizedAccessException("Forbidden");
                }
                //update every store details
                product.ProductName = productDTO.ProductName ?? product.ProductName;
                product.Price = productDTO.Price != 0 ? productDTO.Price: product.Price;

                return await _dataStore.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //update using patch
        public async Task<bool> UpdateProductUsingPatch(PatchProductRequest productDTO, string storeId, string productId)
        {
            try
            {
                //get the store details
                var product = await DisplayProduct(productId);
                //if product is null, return false
                if (product == null)
                {
                    return false;
                }
                //if store id doesnt match, throw exception
                if (product.StoreId != storeId)
                {
                    throw new UnauthorizedAccessException("Forbidden");
                }
                //update only certain details
                //if eg name = new name, then set it to the new name, else set it to previous one
                product.ProductName = productDTO.ProductName ?? product.ProductName;
                product.Price = productDTO.Price != 0 ? productDTO.Price: product.Price;

                return await _dataStore.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //remove product method
        public async Task<bool> RemoveProduct(string productId)
        {
            return await _dataStore.DeleteProduct(productId);
        }
    }
}