using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ETicaret;
using ETicaret.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace ETicaret.Areas.Admin.Controllers
{
    [Authorize(Roles = "Yonetici")]
    public class UyelerController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            return View(context.Users.ToList());
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
