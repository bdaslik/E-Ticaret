using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace ETicaret.Controllers
{
    public class HomeController : Controller
    {
        ESatisEntities db = new ESatisEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View(db.Blog.ToList().OrderByDescending(x => x.UrunID));
        }
        [HttpGet]
        public ActionResult Blog(int sayfa=1)
        {
            return View(db.Blog.ToList().OrderByDescending(x => x.UrunID).ToPagedList(sayfa, 4));
        }

        public ActionResult Contact()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _Slider()
        {
            var slider = db.Slider.Where(x => x.aktif == true);
            return View(slider);
        }

        [ChildActionOnly]
        public ActionResult _Kategori()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _EnYeniler()
        {
            var model = db.Urunler.ToList().OrderByDescending(x=>x.UrunID);
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult _TabMenuUrun()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _OnerilenUrunler()
        {
            var model = db.Oneri.ToList();
            return View(model);
        } 

        [ChildActionOnly]
        public ActionResult _IstekOneri()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult _MailInfo()
        {
            return View();
        }


    }
}