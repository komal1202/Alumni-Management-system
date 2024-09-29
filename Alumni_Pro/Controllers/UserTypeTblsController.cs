using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseContext;

namespace Alumni_Pro.Controllers
{
    public class UserTypeTblsController : Controller
    {
        private ADBEntities db = new ADBEntities();

        // GET: UserTypeTbls
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            return View(db.UserTypeTbls.ToList());
        }

        // GET: UserTypeTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypeTbl userTypeTbl = db.UserTypeTbls.Find(id);
            if (userTypeTbl == null)
            {
                return HttpNotFound();
            }
            return View(userTypeTbl);
        }

        // GET: UserTypeTbls/Create
        public ActionResult Create()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserTypeTbl userTypeTbl)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.UserTypeTbls.Add(userTypeTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userTypeTbl);
        }

        // GET: UserTypeTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypeTbl userTypeTbl = db.UserTypeTbls.Find(id);
            if (userTypeTbl == null)
            {
                return HttpNotFound();
            }
            return View(userTypeTbl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserTypeTbl userTypeTbl)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (ModelState.IsValid)
            {
                db.Entry(userTypeTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userTypeTbl);
        }

        // GET: UserTypeTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserTypeTbl userTypeTbl = db.UserTypeTbls.Find(id);
            if (userTypeTbl == null)
            {
                return HttpNotFound();
            }
            return View(userTypeTbl);
        }

        // POST: UserTypeTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["AdditionalName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            UserTypeTbl userTypeTbl = db.UserTypeTbls.Find(id);
            db.UserTypeTbls.Remove(userTypeTbl);
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
