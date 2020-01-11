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
    public class ContactController : Controller
    {
        private AquariumEntities5 db = new AquariumEntities5();

        //
        // GET: /Contact/

        public ActionResult Index()
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            return View(db.Contact.ToList());
        }

        //
        // GET: /Contact/Details/5

        public ActionResult Details(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // GET: /Contact/Create

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
        // POST: /Contact/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contact.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        //
        // GET: /Contact/Edit/5

        public ActionResult Edit(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contact).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contact);
        }

        //
        // GET: /Contact/Delete/5

        public ActionResult Delete(int id = 0)
        {
            //未登录不允许访问
            if (Session["username"] == null)
            {
                return Content("<script>location.href='/Home/Error';</script>");
            }

            Contact contact = db.Contact.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contact.Find(id);
            db.Contact.Remove(contact);
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