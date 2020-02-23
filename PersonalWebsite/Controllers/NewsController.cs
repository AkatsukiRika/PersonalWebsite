using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebsite.Models;

namespace PersonalWebsite.Controllers
{
    public class NewsController : Controller
    {
        private AquariumEntities2 db = new AquariumEntities2();

        //
        // GET: /News/

        public ActionResult Index()
        {
            //未登录不允许访问
            if(Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            return View(db.News.ToList());
        }

        //
        // GET: /News/Details/5

        public ActionResult Details(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // GET: /News/Create

        public ActionResult Create()
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            return View();
        }

        //
        // POST: /News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(News news)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.News.Add(news);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                return Content("<script>alert('入力した内容に間違いがありました。再度チェックしてください。');</script>");
            }

            return View(news);
        }

        //
        // GET: /News/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(News news)
        {
            if (ModelState.IsValid)
            {
                db.Entry(news).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(news);
        }

        //
        // GET: /News/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            News news = db.News.Find(id);
            if (news == null)
            {
                return HttpNotFound();
            }
            return View(news);
        }

        //
        // POST: /News/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            News news = db.News.Find(id);
            db.News.Remove(news);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}