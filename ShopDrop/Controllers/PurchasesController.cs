﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ShopDrop.Models;

namespace ShopDrop.Controllers
{
    public class PurchasesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Purchases
        public ActionResult Index()
        {
            return View(db.Purchases.ToList());
        }

        // GET: Purchases/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

       

        public ActionResult Orders(string id)
        {
            List<Purchase> purchases = db.Purchases.Where(x => x.buyer_id == id).ToList();
            List<Product> products = new List<Product>();
            foreach (Purchase p in purchases)
            {
                products.Add(p.product);
            }
            return View("Orders", products);
        }

        
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
        [ValidateAntiForgeryToken]
        public ActionResult NewOrder(int productId, int quantity)
        {
            System.Diagnostics.Debug.WriteLine("product:" + productId);
            System.Diagnostics.Debug.WriteLine("quan:" + quantity);
            Product product = db.Products.Find(productId);
            if (quantity > product.Quantity)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            string user_id = User.Identity.GetUserId();
            User user = db.Users.Where(m => m.user_id.Equals(user_id)).First();

            if(User.Identity.GetUserId() == product.selller_id)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(user.balance < product.Price * quantity)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = new Purchase();
            purchase.seller_id = product.selller_id;
            purchase.buyer_id = user.user_id;
            purchase.product = product;
            purchase.quanityBought = quantity;
            db.Purchases.Add(purchase);
            product.Quantity -= quantity;
            user.balance -= quantity * product.Price;
            db.SaveChanges();

            return View("SuccessfulPurchase", purchase);
        }
       
        // GET: Purchases/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Purchase purchase = db.Purchases.Find(id);
            if (purchase == null)
            {
                return HttpNotFound();
            }
            return View(purchase);
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Purchase purchase = db.Purchases.Find(id);
            db.Purchases.Remove(purchase);
            db.SaveChanges();
            return RedirectToAction("Index");
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
