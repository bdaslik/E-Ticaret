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
    public class OneriController : Controller
    {
        private ESatisEntities db = new ESatisEntities();

       
        public ActionResult Index()
        {
            var oneri = db.Oneri.Include(o => o.Urunler);
            return View(oneri.ToList());
        }

        public ActionResult Create()
        {
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OneriID,UrunID")] Oneri oneri)
        {
            if (ModelState.IsValid)
            {
                db.Oneri.Add(oneri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", oneri.UrunID);
            return View(oneri);
        }


        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Oneri oneri = db.Oneri.Find(id);
            if (oneri == null)
            {
                return HttpNotFound();
            }
            return View(oneri);
        }

      
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Oneri oneri = db.Oneri.Find(id);
            db.Oneri.Remove(oneri);
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
