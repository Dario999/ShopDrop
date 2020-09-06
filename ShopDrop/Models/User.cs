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
        private int Id { get; set; }
        public string user_id { get; set; }
        private ICollection<Ad> ads { get; set; }
        private ICollection<Purchase> purchases { get; set; }

        public User()
        {
            ads = new List<Ad>();
            purchases = new List<Purchase>();
        }
    }
}