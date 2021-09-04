using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; //use [Required]
using System.ComponentModel.DataAnnotations.Schema;

namespace Management.Models
{
    public class Store
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string StoreName {get; set; }
        public string StoreNumber { get; set; }
        public string StoreType {get; set; }
        public int StoreProducts {get; set; }

        //store has many products
        public ICollection<Product> Product { get; set; }
    }
}
