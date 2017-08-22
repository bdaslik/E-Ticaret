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
    public class StokController : Controller
    {
        private ESatisEntities db = new ESatisEntities();
        
        public ActionResult Index()
        {
            var stok = db.Stok.Include(s => s.Urunler);
            return View(stok.ToList().OrderByDescending(x=>x.UrunID));
        }
        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stok stok = db.Stok.Find(id);
            if (stok == null)
            {
                return HttpNotFound();
            }
            return View(stok);
        }
        
        public ActionResult Create()
        {
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StokID,UrunID,Adet")] Stok stok)
        {
            if (ModelState.IsValid)
            {
                db.Stok.Add(stok);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", stok.UrunID);
            return View(stok);
        }
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stok stok = db.Stok.Find(id);
            if (stok == null)
            {
                return HttpNotFound();
            }
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", stok.UrunID);
            return View(stok);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StokID,UrunID,Adet")] Stok stok)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stok).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", stok.UrunID);
            return View(stok);
        }

        // GET: Admin/Stok/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Stok stok = db.Stok.Find(id);
            if (stok == null)
            {
                return HttpNotFound();
            }
            return View(stok);
        }

        // POST: Admin/Stok/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Stok stok = db.Stok.Find(id);
            db.Stok.Remove(stok);
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
