using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    public class Category
    {
        public ICollection<string> categories { get; set; }

        public Category()
        {
            categories = new List<string>();
        }
    }
}