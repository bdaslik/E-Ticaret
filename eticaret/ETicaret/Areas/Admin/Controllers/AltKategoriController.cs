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
    public class AltKategoriController : Controller
    {
        private ESatisEntities db = new ESatisEntities();

        // GET: Admin/AltKategori
        public ActionResult Index()
        {
            var alt_Kategori = db.Alt_Kategori.Include(a => a.Kategori);
            return View(alt_Kategori.ToList());
        }
        
        // GET: Admin/AltKategori/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAd");
            return View();
        }

        // POST: Admin/AltKategori/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AK_ID,KategoriID,Ad")] Alt_Kategori alt_Kategori)
        {
            if (ModelState.IsValid)
            {
                db.Alt_Kategori.Add(alt_Kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAd", alt_Kategori.KategoriID);
            return View(alt_Kategori);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            Alt_Kategori alt_Kategori = db.Alt_Kategori.Find(id);
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAd", alt_Kategori.KategoriID);
            return View(alt_Kategori);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Alt_Kategori model)
        {
            if (ModelState.IsValid)
            {
                var attached = db.Alt_Kategori.Attach(model);
                db.Entry(attached).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "KategoriID", "KategoriAd", model.KategoriID);
            return View(model);
        }

        // GET: Admin/AltKategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alt_Kategori alt_Kategori = db.Alt_Kategori.Find(id);
            if (alt_Kategori == null)
            {
                return HttpNotFound();
            }
            return View(alt_Kategori);
        }

        // POST: Admin/AltKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alt_Kategori alt_Kategori = db.Alt_Kategori.Find(id);
            db.Alt_Kategori.Remove(alt_Kategori);
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
