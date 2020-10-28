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
                var obj = db.Users.Where(x => x.Password.Equals(usrs.Password) &&
                                              x.Username.Equals(usrs.Username))
                                  .FirstOrDefault();
                if (obj != null)
                {
                    Session["usrId"] = usrs.Id;
                    Session["LogPassword"] = usrs.Password.ToString();
                    Session["LogUserName"] = usrs.Username.ToString();

                    return RedirectToAction ("LoggedIn");
                }
                    
            return View(usrs);
        }

        
        public ActionResult LoggedIn(Users usr)
        {
            if (Session["LogUserName"] != null && Session["LogPassword"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LogIn");
        }

        public ActionResult LogOut()
        {
            if (Session["LogUserName"] != null && Session["LogPassword"] != null)
            {
                Session["LogUserName"] = null;
                Session["LogPassword"] = null;
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LogIn");
        }
    }
}