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
        public int Id { get; set; }
        public string user_id { get; set; }
        private ICollection<Product> products { get; set; }
        private ICollection<Purchase> purchases { get; set; }
        public double balance { get; set; }

        public User()
        {
            products = new List<Product>();
            purchases = new List<Purchase>();
        }
    }
}