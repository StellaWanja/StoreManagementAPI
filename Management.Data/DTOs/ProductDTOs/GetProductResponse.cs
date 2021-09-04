using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Data.DTOs.ProductDTOs
{
    public class GetProductResponse
    {
        public string Id { get; set; }
        public string StoreId { get; set; }
        public string ProductName {get;set;}
        public int Price {get; set; }
    }
}
