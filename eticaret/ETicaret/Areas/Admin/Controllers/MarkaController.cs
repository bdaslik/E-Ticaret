using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETicaret;

namespace ETicaret.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    public class MarkaController : Controller
    {
        private ESatisEntities db = new ESatisEntities();

        // GET: Admin/Marka
        public ActionResult Index()
        {
            return View(db.Marka.ToList());
        }



        // GET: Admin/Marka/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Marka/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MarkaID,Marka1")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                db.Marka.Add(marka);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marka);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var marka = db.Marka.Find(id);

            return View(marka);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Marka model)
        {

            if (ModelState.IsValid)
            {
                var attached = db.Marka.Attach(model);
                db.Entry(attached).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Marka/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Marka marka = db.Marka.Find(id);
            if (marka == null)
            {
                return HttpNotFound();
            }
            return View(marka);
        }

        // POST: Admin/Marka/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Marka marka = db.Marka.Find(id);
            db.Marka.Remove(marka);
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
