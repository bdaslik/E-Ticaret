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
    public class BlogController : Controller
    {
        private ESatisEntities db = new ESatisEntities();

        
        public ActionResult Index()
        {
            var blog = db.Blog.Include(b => b.Urunler);
            return View(blog.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }



        public ActionResult Add()
        {
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model");
            return View();
        }
        
        const string imgpath = "/Content/slider/";
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBlog(BlogModel model)
        {

            if (ModelState.IsValid)
            {
                Blog blog = new Blog();
                //Dosya Kaydetme
                if (model.Resim != null && model.Resim.ContentLength > 0)
                {
                    var filename = model.Resim.FileName;
                    var path = Path.Combine(Server.MapPath("~" + imgpath), filename);
                    model.Resim.SaveAs(path);
                    blog.BlogResim = imgpath + filename;
                }
                blog.Baslik = model.Baslik;
                blog.Metin = model.Metin;
                blog.tarih = model.tarih;
                blog.UrunID = model.UrunID;

                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", model.UrunID);
            return View(model);
        }



        public ActionResult Create()
        {
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BlogID,UrunID,Baslik,Metin,tarih,BlogResim")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Blog.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", blog.UrunID);
            return View(blog);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            
            Blog blog = db.Blog.Find(id);
           
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", blog.UrunID);
            return View(blog);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Blog model)
        {
            if (ModelState.IsValid)
            {
                var attached = db.Blog.Attach(model);
                db.Entry(attached).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UrunID = new SelectList(db.Urunler, "UrunID", "Model", model.UrunID);
            return View(model);
        }

        // GET: Admin/Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Admin/Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
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
