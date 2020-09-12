using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
        [DisplayName("Upload File")]
        public string Image { get; set; }
        public string category { get; set; }
        public DateTime date_posted { get; set; }
        public string selller_id { get; set; }
        public string sellerName { get; set; }
        public string description { get; set; }

        public Product()
        {
            date_posted = DateTime.Now;
        }
    }
}