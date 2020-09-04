using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public float Balance { get; set; }
        public virtual List<Product> Products { get; set; }

        public ShoppingCart()
        {
            Products = new List<Product>();
        }
    }
}