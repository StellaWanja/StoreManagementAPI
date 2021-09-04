using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Management.Data.DTOs.StoreDTOs
{
    public class PatchStoreRequest
    {
        public string StoreName {get; set; }
        public string StoreNumber { get; set; }
        public string StoreType {get; set; }
        public int StoreProducts {get; set; }
    }
}
