using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Management.Data.DTOs.ProductDTOs
{
    public class PutProductRequest
    {
        [Required]
        public string ProductName {get;set;}
        [Required]
        public int Price {get; set; }
    }
}
