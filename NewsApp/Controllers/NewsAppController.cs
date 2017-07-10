using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using PagedList;


namespace NewsApp.Controllers
{
    public class NewsAppController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewsStories

        public ActionResult Index(int? page)
        {
            var today = DateTime.Today;
            var newsstories = db.NewsStories
                 .Where(p => p.DateCreated < today)
                 .OrderByDescending(p => p.DateCreated)
                 .ToList();

            int pageSize = 6;
            int pageNumber = (page ?? 1);
            return View(newsstories.ToPagedList(pageNumber,pageSize));
        }

        // GET: NewsStories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewsStory newsStory = db.NewsStories.Find(id);

            if (newsStory == null)
            {
                return HttpNotFound();
            }
            return View(newsStory);
        }
    }
}
