using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PaperBook.DAL;
using PaperBook.Models;

namespace PaperBook.Controllers
{
    public class ArticleController : Controller
    {
        private BookContext db = new BookContext();

        // GET: Article
        public ActionResult Index(string sortOrder)
        {
            var articles = db.Articles.Include(a => a.Author).Include(a => a.Category);

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            articles = from s in articles select s;
            switch (sortOrder)
            {
                case "title_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    articles = articles.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    articles = articles.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }
            return View(articles.ToList());
        }

        // GET: Article/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Article/Create
        public ActionResult Create()
        {
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName");
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            return View();
        }

        // POST: Article/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,CreatedDate,Views,AuthorID,CategoryID")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", article.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            return View(article);
        }

        // GET: Article/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", article.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            return View(article);
        }

        // POST: Article/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,CreatedDate,Views,AuthorID,CategoryID")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorID = new SelectList(db.Authors, "AuthorID", "FirstName", article.AuthorID);
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", article.CategoryID);
            return View(article);
        }

        // GET: Article/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Article/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
