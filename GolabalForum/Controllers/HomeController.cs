using GolabalForum.Models;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GolabalForum.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? page)
        {
            ForumModelDb db = new ForumModelDb();

            var data = db.Categories
                         .OrderBy(x => x.Id)
                         .Include(path: x => x.Topics);
            var pageNum = page ?? 1;
            int pageSize = 5;


            return View(data.ToPagedList(pageNum, pageSize));
        }
    }
}