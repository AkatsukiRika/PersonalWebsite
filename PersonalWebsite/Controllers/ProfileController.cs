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
    public class ProfileController : Controller
    {
        private AquariumEntities3 db = new AquariumEntities3();

        //
        // GET: /Profile/

        public ActionResult Index()
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            return View(db.Profile.ToList());
        }

        //
        // GET: /Profile/Details/5

        public ActionResult Details(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // GET: /Profile/Create

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
        // POST: /Profile/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profile.Add(profile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(profile);
        }

        //
        // GET: /Profile/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profile/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        //
        // GET: /Profile/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Profile profile = db.Profile.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }
            return View(profile);
        }

        //
        // POST: /Profile/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profile profile = db.Profile.Find(id);
            db.Profile.Remove(profile);
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