using System;
using System.ComponentModel.DataAnnotations; //use [Required]
using System.ComponentModel.DataAnnotations.Schema;

namespace Management.Models
{
    //hold product info
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string StoreId { get; set; }
        public string ProductName {get;set;}
        public int Price {get; set; }
        //navigational property
        public Store Store { get; set; }
    }
}