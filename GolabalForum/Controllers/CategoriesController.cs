using GolabalForum.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GolabalForum.Controllers
{
    public class CategoriesController : Controller
    {
        private ForumModelDb db = new ForumModelDb();

        // GET: Categories
        public ActionResult Index(string search, int? Page)
        {
            // load Catgories from database as queryable
            var categories = db.Categories.AsQueryable();

            // check sent search and store it in variable
            if (!string.IsNullOrEmpty(search))
            {
                categories = categories.Where(x => x.Name.StartsWith(search) || x.Name.Contains(search));
            }

            var data = categories.OrderBy(s => s.Id).Include(x => x.Topics);

            // variables to make paged list
            int PageSize = 8;
            var PageNum = Page ?? 1;

            ViewBag.Search = search;

            return View(data.ToPagedList(PageNum, PageSize));
        }
// ========================================================================================= //

        // Create
        public ActionResult Create( Categories cat)
        {
            // check if new categories is exist add new model state
            var TestNewCategory = db.Categories.Where(x => x.Name == cat.Name);
            if (TestNewCategory.Any())
            {
                ModelState.AddModelError("Name", "this name is exist , please select another name !");
            }

            // check model state and save changes to database
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();

                // redirect to index and send meseege
                TempData["msg"] = "new Categories has been saved.";
                return RedirectToAction("Index");
            }
            return View(cat);
        }
// ========================================================================================= //
        
        // Update
        public ActionResult Edit( int id)
        {
            var cat2Edit = db.Categories.FirstOrDefault(x => x.Id == id);
            if (cat2Edit == null)
            {
                return RedirectToAction("Index");
            }
            return View(cat2Edit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Categories category)
        {
            if (ModelState.IsValid)
            {
                var cat2Edit = db.Categories.FirstOrDefault(x => x.Id == category.Id);
                cat2Edit.Name = category.Name;
                db.SaveChanges();

                TempData["msg"] = "You have updated your Category : " + "" + cat2Edit.Name+ "" + ", Successfully !";
                return RedirectToAction("Index");
            }
            return View(category);
        }
// ========================================================================================== //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var cat2Del = db.Categories.Find(id);
            db.Categories.Remove(cat2Del);
            TempData["msg"] = "you have been deleted : "+"" + cat2Del.Name +" " + "Category Successfully !";
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}