using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    public class Purchase
    {

        [Key]
        public int id { get; set; }
        public string user_id { get; set; }
        public int product_id { get; set; }

    }
}