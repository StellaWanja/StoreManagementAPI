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
    public interface IStoreDataStore
    {
        //add stores
        Task<Store> AddStore(Store store);
        //display of a store
        Task<Store> GetStore(string storeId);
        //update store
        Task<bool> UpdateStore(Store store);
        //delete store
        Task<bool> DeleteStore(string storeId);
    }
}