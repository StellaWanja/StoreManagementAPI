using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Management.Data.DTOs.ProductDTOs
{
    public class PatchProductRequest
    {
        public string ProductName {get;set;}
        public int Price {get; set; }
    }
}
