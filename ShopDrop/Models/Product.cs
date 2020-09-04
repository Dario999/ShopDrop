using System;
using System.Collections.Generic;
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
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
        public string Image { get; set; }
        public Category Category { get; set; }
        public ICollection<Category> Categories { get; set; }

        public Product()
        {
            ShoppingCarts = new List<ShoppingCart>();
        }
    }
}