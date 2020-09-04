using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    public class User
    {

        [Key]
        private string id { get; set; }
        private ICollection<Product> products { get; set; }
        private ICollection<Purchase> purchases { get; set; }

        public User()
        {
            products = new List<Product>();
            purchases = new List<Purchase>();
        }
    }
}