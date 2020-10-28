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
    public class TopicsController : Controller
    {
        private ForumModelDb _db { get; set; }

        public TopicsController()
        {
            _db = new ForumModelDb();
        }


        // GET: Topics
        [Route("~/Category-{CategoryId}")]
        public ActionResult Index(int categoryId, int? page)
        {
            var cat = _db.Categories.FirstOrDefault(x => x.Id == categoryId);

            if (cat == null)
            {
                return HttpNotFound();
            }

            ViewBag.categoryName = cat.Name;
            var data = _db.Topics
                          .Include(t => t.Auther)
                          .Where(t => t.CategoryId == categoryId)
                          .OrderBy(x => x.CreatedAt);

            var pageNum = page ?? 1;
            var pageSize = 5;

            return View(data.ToPagedList(pageNum, pageSize));
        }

        // ======================================================================================== //
        // Method to Show Details Topic
        [Route("~/Topic-{topicId}")]
        public ActionResult Show(int topicId, int? page)
        {
            var topic = _db.Topics.Include(x => x.Auther)
                                  .Include(c => c.Categories)
                                  .FirstOrDefault(s => s.Id == topicId);
            if (topic == null)
            {
                return HttpNotFound();
            }else

            topic.TotalViews += 1;
            _db.SaveChanges();

            ViewBag.comments = _db.Comments.Where(x => x.TopicId == topicId)
                                           .Include(x => x.Topics.Auther)
                                           .OrderBy(x => x.CreatedAt);

            return View(topic);
        }

        // ========================================================================================== //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(Comments addcomment, string comment, int usrId, int topicId)
        {
            _db.Comments.Add(addcomment).Body = comment;
            _db.Comments.Add(addcomment).CreatedBy = usrId;
            _db.Comments.Add(addcomment).TopicId = topicId;
            _db.Comments.Add(addcomment).CreatedAt = DateTime.Now;
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}