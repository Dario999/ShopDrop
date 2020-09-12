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
        public ActionResult ShowMyProducts()
        {
            return View(db.Products.ToList());
        }
        public ActionResult ListByCategory(String Category)
        {
            if(Category == "All")
            {
                return View("Index",db.Products.ToList().FindAll(x=>x.selller_id != @User.Identity.GetUserId()));
            }
            return View("Index", db.Products.ToList().FindAll(x => x.category == Category && x.selller_id != @User.Identity.GetUserId()));
        }


        public ActionResult ListByCategoryAndSearch(String Category,String text)
        {
            if (Category == "All")
            {
                if(text == "")
                {
                    return View("Index", db.Products.ToList().FindAll(x => x.selller_id != @User.Identity.GetUserId()));
                }
                return View("Index", db.Products.Where(x => x.Name.ToLower().Contains(text.ToLower())).ToList().FindAll(x => x.selller_id != @User.Identity.GetUserId()));
            }
            return View("Index", db.Products.ToList().FindAll(x => x.selller_id != @User.Identity.GetUserId() && x.category == Category && x.Name.ToLower().Contains(text.ToLower())));
        }

        public ActionResult ListByCategoryMyProducts(String Category)
        {
            if (Category == "All")
            {
                return View("Index", db.Products.ToList().FindAll(x => x.selller_id == @User.Identity.GetUserId()));
            }
            return View("Index", db.Products.ToList().FindAll(x => x.category == Category && x.selller_id == @User.Identity.GetUserId()));
        }


        public ActionResult ListByCategoryAndSearchMyProducts(String Category, String text)
        {
            if (Category == "All")
            {
                if (text == "")
                {
                    return View("ShowMyProducts", db.Products.ToList().FindAll(x => x.selller_id == @User.Identity.GetUserId()));
                }
                return View("ShowMyProducts", db.Products.Where(x => x.Name.ToLower().Contains(text.ToLower())).ToList().FindAll(x => x.selller_id == @User.Identity.GetUserId()));
            }
            return View("ShowMyProducts", db.Products.ToList().FindAll(x => x.selller_id == @User.Identity.GetUserId() && x.category == Category && x.Name.ToLower().Contains(text.ToLower())));
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
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Quantity,Image,category,description")] Product product, HttpPostedFileBase ImageFile)
        {

            if (ImageFile != null)
            {
                string trailingPath = Path.GetFileName(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                trailingPath = computeHash(trailingPath + User.Identity.Name);
                trailingPath = DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + "_" + trailingPath + extension;
                string fullPath = Path.Combine(Server.MapPath("~/UserImages"), trailingPath);
                product.Image = trailingPath;
                product.selller_id = User.Identity.GetUserId();
                product.sellerName = User.Identity.GetUserName();
                ImageFile.SaveAs(fullPath);

            }
            else
            {
                product.Image = "placeholder-image.png";
                product.selller_id = User.Identity.GetUserId();
                product.sellerName = User.Identity.GetUserName();
            }
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("ShowMyProducts");
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
            if(product.selller_id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Quantity,Image,category")] Product product, HttpPostedFileBase ImageFile)
        {
            product.selller_id = User.Identity.GetUserId();
            product.sellerName = User.Identity.GetUserName();
      
            if (ImageFile != null)
            {
                string trailingPath = Path.GetFileName(ImageFile.FileName);
                string extension = Path.GetExtension(ImageFile.FileName);
                trailingPath = computeHash(trailingPath + User.Identity.Name);
                trailingPath = DateTime.Now.ToString("yyyy-MM-dd-hh-mm") + "_" + trailingPath + extension;
                string fullPath = Path.Combine(Server.MapPath("~/UserImages"), trailingPath);
                product.Image = trailingPath;
              
                ImageFile.SaveAs(fullPath);
            }
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ShowMyProducts");
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
            if (product.selller_id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized);
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("ShowMyProducts");
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