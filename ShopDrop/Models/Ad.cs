using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    public class Ad
    {

        [Key]
        public int Id { get; set; }
        //public int product_id { get; set; }
        public virtual Product product { get; set; }
        public DateTime date_posted { get; set; }
        public bool is_sold { get; set; }
        public string seller_id { get; set; }

        public Ad()
        {
            date_posted = DateTime.Now;
        }

    }
}