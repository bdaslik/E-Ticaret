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
    public class SlidersController : Controller
    {
        private ESatisEntities db = new ESatisEntities();

        // GET: Admin/Sliders
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: Admin/Sliders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }

            return View(slider);
        }
        
        public ActionResult Add()
        {
            return View();
        }



        const string imgpath= "/Content/slider/";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSlider(SliderModel model)
        {
            
            if (ModelState.IsValid)
            {
                Slider slider = new Slider();
                //Dosya Kaydetme
                if (model.Resim != null && model.Resim.ContentLength > 0) 
                {
                    var filename = model.Resim.FileName;
                    var path = Path.Combine(Server.MapPath("~"+imgpath), filename);
                    model.Resim.SaveAs(path);
                    slider.ResimYolu = imgpath + filename;
                }
                slider.aktif = model.aktif;
                slider.Baslik = model.Baslik;
                slider.Fiyat = model.Fiyat;
                slider.Mesaj = model.Mesaj;
                slider.link = model.link;
                
                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var slider = db.Slider.Find(id);
           
            return View(slider);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Slider model)
        {

            if (ModelState.IsValid)
            {
                var attached=db.Slider.Attach(model);
                db.Entry(attached).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: Admin/Sliders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Admin/Sliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            db.Slider.Remove(slider);
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
