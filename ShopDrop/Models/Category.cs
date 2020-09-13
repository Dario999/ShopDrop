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

        public static List<string> getAllCategories()
        {
            return new List<string>() { "Vehicles", "Real Estate", "Home and Garden", "Clothing and Footwear", "Technology", "Sport Equipment"
                ,"Watches and Jewelry","Books and Literature","Health and Beauty","Machines and Tools","Food and Cooking" ,"Other"
            };
        }
    }
}