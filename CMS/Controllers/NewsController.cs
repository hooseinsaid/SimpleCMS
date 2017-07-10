using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CMS.Models;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using PagedList;

namespace CMS.Controllers
{
    [Authorize(Roles = "User")]
    public class NewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: News
        public ActionResult Index(int? page)
        {

            string loggedInUserId = User.Identity.GetUserId();
            List<NewsStory> newsStories = (from r in db.NewsStories where r.UserId == loggedInUserId select r).ToList();

            int pageSize = 15;
            int pageNumber = (page ?? 1);
            return View(newsStories.ToPagedList(pageNumber, pageSize));
        }

        // GET: News/Details/5
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

        // GET: News/Create
        [Authorize(Roles = "User")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "Title,Content")] NewsStory newsStory, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    //Creating new Image entity
                    var newsImage = new Image
                    {
                        ImageName = System.IO.Path.GetFileName(image.FileName),
                    };
                    
                    //Setting ImageContent as a byte array of the image file
                    using (var reader = new System.IO.BinaryReader(image.InputStream))
                    {
                        newsImage.ImageContent = reader.ReadBytes(image.ContentLength);
                    }

                    //Resizing the image
                    WebImage resizedImage = new WebImage(newsImage.ImageContent);
                    resizedImage.Resize(300, 200,false);
                    newsImage.ImageContent = resizedImage.GetBytes();
                    newsStory.Image = newsImage;
                }
                
                //Creating a List to hold the KeyWord objects that are going to be associated with the current NewsStory
                List<KeyWord> allkeywords = new List<KeyWord>();

                var inputKeywords = Request["KeyWords"];
                var keywords = inputKeywords.Split(',').Select(p=>p.Trim());
                foreach (var word in keywords)
                { 
                    KeyWord keyword = new KeyWord();
                    keyword.keyWordContent = word;

                    bool exists = db.KeyWords.AsEnumerable().Where(c => c.keyWordContent.Equals(word)).Count() > 0;

                    /*Checking if the keyword already exists in the KeyWords DB table 
                    and if exists, mapping the current NewsStory to the existing keywords*/
                    if (exists)
                    {
                        var existingkeys = db.KeyWords.Where(c => c.keyWordContent.Equals(word)).ToList();
                        foreach (var kw in existingkeys)
                        {
                            allkeywords.Add(kw);
                        }
                    }
                    else
                    {
                        allkeywords.Add(keyword);
                    }    
                }

                newsStory.KeyWords = allkeywords;
                newsStory.DateCreated = DateTime.Now;
                newsStory.UserId = User.Identity.GetUserId();
              
                db.NewsStories.Add(newsStory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(newsStory);
        }

        // GET: News/Edit/5
        [Authorize(Roles = "User")]
        public ActionResult Edit(int? id)
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

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "NewsStoryID,UserId,Title,Content,DateCreated")] NewsStory newsStory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newsStory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(newsStory);
        }

        // GET: News/Delete/5
        [Authorize(Roles = "User")]
        public ActionResult Delete(int? id)
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

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public ActionResult DeleteConfirmed(int id)
        {
            NewsStory newsStory = db.NewsStories.Find(id);
            Image image = db.Images.Find(id);
            db.NewsStories.Remove(newsStory);
            db.Images.Remove(image);
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
