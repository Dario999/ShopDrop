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
        public string buyer_id { get; set; }
        public string seller_id { get; set; }
        public int productId { get; set; }
        public int quanityBought { get; set; }

    }
}