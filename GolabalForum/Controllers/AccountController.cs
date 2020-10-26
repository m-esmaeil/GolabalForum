using GolabalForum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GolabalForum.Controllers
{
    public class AccountController : Controller
    {
        private ForumModelDb db = new ForumModelDb();
        // GET: Account
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Registeration()
        {
            return View();
        }

        // Post Method for Registeration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registeration(Users usrs)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(usrs);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "some Error occured");
            }
            return View("usrs");
        }

        // make log in method
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogIn(Users usrs)
        {
            if (ModelState.IsValid)
            {
                var obj = db.Users.Where(x => x.Password == usrs.Password && x.Username == usrs.Username).FirstOrDefault();
                if (obj != null)
                {
                    Session["Password"] = usrs.Password.ToString();
                    Session["UserName"] = usrs.Username.ToString();

                    return RedirectToAction ("LoggedIn");
                }
            }
            return View(usrs);
        }

        
        public ActionResult LoggedIn()
        {
            if (Session["UserName"] != null)
            {
                return PartialView("_LogInView");
            }
            return RedirectToAction("LogIn");
        }
    }
}