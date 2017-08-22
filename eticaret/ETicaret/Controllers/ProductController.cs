using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Controllers
{
    public class ProductController : Controller
    {
        ESatisEntities db = new ESatisEntities();
        // GET: Product
        public ActionResult Details(int id)
        {
            var model = db.Urunler.Find(id);
            return View(model);
        }

        public ActionResult Shop(int id)
        {
            
            return View(db.Urunler.Where(x => x.AK_ID == id));
        }
        [HttpGet]
        public ActionResult Shop(int id,int sayfa=1)
        {
            return View(db.Urunler.Where(x => x.AK_ID == id).OrderByDescending(x=>x.UrunID).ToPagedList(sayfa, 6));
        }

        public ActionResult MShop(int id)
        {

            return View(db.Urunler.Where(x => x.MarkaID == id));
        }
        [HttpGet]
        public ActionResult MShop(int id, int sayfa = 1)
        {
            return View(db.Urunler.Where(x => x.MarkaID == id).OrderByDescending(x => x.UrunID).ToPagedList(sayfa, 6));
        }

    }
}