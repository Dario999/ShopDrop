using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ShopDrop.Models
{
    [NotMapped]
    public class Category
    {

        public Category()
        {
        }

        public static List<String> getAllCategories()
            
        {
            return new List<string> {
                "Technology",
                "Auto", 
                "Beauty Products",
                "Household",
                "Food",
                "Clothes",
                "Books",
                "Sports"
            };
        }
    }
}