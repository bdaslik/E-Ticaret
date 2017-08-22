using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETicaret;
using ETicaret.Areas.Admin.Models;
using System.IO;

namespace ETicaret.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    public class UrunlersController : Controller
    {
        private ESatisEntities db = new ESatisEntities();

        
        public ActionResult Index()
        {
            var urunler = db.Urunler.Include(u => u.Marka).Include(u => u.Alt_Kategori);
            return View(urunler.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }
        
        public ActionResult Create()
        {
            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "Marka1");
            ViewBag.AK_ID = new SelectList(db.Alt_Kategori, "AK_ID", "Ad");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UrunID,Model,Fiyat,MarkaID,Ozellikler,Aciklama,Tarih,imgpath,AK_ID")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Urunler.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "Marka1", urunler.MarkaID);
            ViewBag.AK_ID = new SelectList(db.Alt_Kategori, "AK_ID", "Ad", urunler.AK_ID);
            return View(urunler);
        }

        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "Marka1", urunler.MarkaID);
            ViewBag.AK_ID = new SelectList(db.Alt_Kategori, "AK_ID", "Ad", urunler.AK_ID);
            return View(urunler);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UrunID,Model,Fiyat,MarkaID,Ozellikler,Aciklama,Tarih,imgpath,AK_ID")] Urunler urunler)
        {
            if (ModelState.IsValid)
            {
                db.Entry(urunler).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MarkaID = new SelectList(db.Marka, "MarkaID", "Marka1", urunler.MarkaID);
            ViewBag.AK_ID = new SelectList(db.Alt_Kategori, "AK_ID", "Ad", urunler.AK_ID);
            return View(urunler);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunler.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urunler urunler = db.Urunler.Find(id);
            db.Urunler.Remove(urunler);
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
