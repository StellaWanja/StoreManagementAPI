using Management.Models;
using Management.Data.DTOs.StoreDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Data.DTOs.Mappings
{

    // structure of the response 
    public class StoreMappings
    {
        public static AddStoreResponse AddStoreResponse(Store store)
        {
            return new AddStoreResponse
            {
                Id = store.Id,
                UserId = store.UserId,
                StoreName = store.StoreName,
                StoreNumber = store.StoreNumber,
                StoreType = store.StoreType,
                StoreProducts = store.StoreProducts
            };
        }

        public static GetStoreResponse GetStoreResponse(Store store)
        {
            return new GetStoreResponse
            {
                Id = store.Id,
                UserId = store.UserId,
                StoreName = store.StoreName,
                StoreNumber = store.StoreNumber,
                StoreType = store.StoreType,
                StoreProducts = store.StoreProducts,
            };
        }
    }
}