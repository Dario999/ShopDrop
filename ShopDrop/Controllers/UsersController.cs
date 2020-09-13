using System;
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
    [Authorize]
    public class UsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Balance()
        {
            string user_id = User.Identity.GetUserId();
            User user = db.Users.FirstOrDefaultAsync(x => x.user_id == user_id).Result;
            if (user != null)
            {
                return View(user);
            }
            else return new HttpStatusCodeResult(HttpStatusCode.NotFound);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBalance(double balance)
        {
            string user_id = User.Identity.GetUserId();
            User user = db.Users.FirstOrDefaultAsync(x => x.user_id == user_id).Result;
            if (balance < 100.0 || Request.Form["balance"] == null || string.IsNullOrWhiteSpace(Request.Form["balance"]))
            {
                TempData["ErrorMessage"] = "Please insert a value bigger than 20$";
                return RedirectToAction("Balance", "Users", "");
            }
            else if(balance >= 100.0)
            {
                user.balance += balance;
            }
            db.SaveChanges();
            return View("Balance", user);

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
