using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using ShopDrop.Models;

namespace ShopDrop.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult ListByCategory(string Category)
        {
            return View("Index", db.Products.ToList().FindAll(x => x.category == Category));
        }

        private String computeHash(String fileName)
        {
            return String.Format("{0:X}", fileName.GetHashCode());

        }
        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity,Image,category")] Product product, HttpPostedFileBase ImageFile)
        {
            
            
            string trailingPath = Path.GetFileName(ImageFile.FileName);
            string extension = Path.GetExtension(ImageFile.FileName);
            trailingPath = computeHash(trailingPath + User.Identity.Name);
            trailingPath = DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + "_" +trailingPath + extension;
            string fullPath = Path.Combine(Server.MapPath("~/UserImages"), trailingPath);
            product.Image = trailingPath;
            ImageFile.SaveAs(fullPath);

            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Quantity,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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

        public ActionResult CreateAd(int? id) 
        {
            Ad add = new Ad();
            add.product = db.Products.Find(id);
            add.is_sold = false;
            add.date_posted = DateTime.Now;
            add.seller_id = User.Identity.GetUserId();
            
            db.Ads.Add(add);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult ShowMyProducts()
        {
            string userId = User.Identity.GetUserId();
            User user = db.Users.Where(s => s.user_id == userId).First();
            return View(db.Products.Where(s => s.selller_id == user.Id).First());
        }

    }
}
