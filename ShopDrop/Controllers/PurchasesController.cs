using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ShopDrop.Models;

namespace ShopDrop.Controllers
{
    [Authorize]
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Orders()
        {
            String user_id = @User.Identity.GetUserId();
            List<Purchase> purchases = db.Purchases.Where(x => x.buyer_id == user_id).ToList();
            List<Product> products = new List<Product>();
            foreach (Purchase p in purchases)
            {
                foreach (Product product in db.Products)
                {
                    if ((p as Purchase).productId == product.Id)
                    {
                        product.Quantity = p.quanityBought;
                        product.date_posted = DateTime.Now;
                        products.Add(product);
                    }   
                }
            }
            return View("Orders", products);
        }

        [Authorize]
        public ActionResult NewOrder(int productId)
        {
            Product product = db.Products.Find(productId);
            if(product.selller_id == User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(product);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(int productId, int quantity)
        {
            System.Diagnostics.Debug.WriteLine("product:" + productId);
            System.Diagnostics.Debug.WriteLine("quan:" + quantity);
            Product product = db.Products.Find(productId);
            if (quantity > product.Quantity)
            {
                TempData["MaxQuantity"] = "You can't buy more than maximum quantity";
                return RedirectToAction("NewOrder", "Purchases", new { productId = product.Id });
            }

            string user_id = User.Identity.GetUserId();
            User user = db.Users.Where(m => m.user_id.Equals(user_id)).First();
            User seller = db.Users.Where(m => m.user_id.Equals(product.selller_id)).First();

            if(User.Identity.GetUserId() == product.selller_id)
            {
                return RedirectToAction("NewOrder", "Purchases", new { productId = product.Id });
            }
            if(user.balance < product.Price * quantity)
            {
                TempData["MoneyError"] = "You don't have enough money. Please add to your balance.";
                return RedirectToAction("NewOrder", "Purchases", new { productId = product.Id });
            }
            Purchase purchase = new Purchase();
            purchase.seller_id = product.selller_id;
            purchase.buyer_id = user.user_id;
            purchase.productId = product.Id;
            purchase.quanityBought = quantity;
            db.Purchases.Add(purchase);
            product.Quantity -= quantity;
            user.balance -= quantity * product.Price;
            seller.balance += quantity * product.Price;
            db.SaveChanges();

            return View("SuccessfulPurchase", purchase);
        }
       
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
