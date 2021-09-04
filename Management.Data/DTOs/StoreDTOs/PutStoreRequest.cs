using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Management.Data.DTOs.StoreDTOs
{
    public class PutStoreRequest
    {
        [Required]
        public string StoreName {get; set; }
        [Required]
        public string StoreNumber { get; set; }
        [Required]
        public string StoreType {get; set; }
        [Required]
        public int StoreProducts {get; set; }
    }
}
