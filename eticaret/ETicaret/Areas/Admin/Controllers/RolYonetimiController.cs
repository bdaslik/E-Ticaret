using ETicaret.Areas.Admin.Models;
using ETicaret.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ETicaret.Areas.Admin.Controllers
{
    [Authorize(Roles ="Yonetici")]
    public class RolYonetimiController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: Admin/RolYonetimi
        public ActionResult Index()
        {
            var rolStore = new RoleStore<IdentityRole>(context);
            var rolManager = new RoleManager<IdentityRole>(rolStore);

            var model = rolManager.Roles.ToList();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RolEkleModel rol)
        {
            var rolStore = new RoleStore<IdentityRole>(context);
            var rolManager = new RoleManager<IdentityRole>(rolStore);

            if (rolManager.RoleExists(rol.rolAd)==false)
            {
                rolManager.Create(new IdentityRole(rol.rolAd));
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult AddtoUser()
        {
            ViewBag.KategoriID = new SelectList(context.Roles, "Id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddtoUser(RolKullaniciEkleModel model)
        {
            var rolStore = new RoleStore<IdentityRole>(context);
            var rolManager = new RoleManager<IdentityRole>(rolStore);

            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var kullanici = userManager.FindByName(model.kullaniciAdi);

            if (!userManager.IsInRole(kullanici.Id,model.rolAd))
            {
                userManager.AddToRole(kullanici.Id, model.rolAd);
                return RedirectToAction("Index");
            }

            return View();
        }

    }
}