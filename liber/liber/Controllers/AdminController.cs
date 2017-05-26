using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using liber.Models;
using System.IO;
using System.Web.WebPages.Html;

namespace liber.Controllers
{
    public class AdminController : Controller
    {
        string consulta;
        Banners banner = new Banners();
        // GET: Admin
        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult Banners()
        {
            consulta = "SeleccionarBanners";
            Dictionary<int, Banners> dicBanner = banner.MostrarBanners(consulta);
            ViewBag.dicBanner = dicBanner;
            return View();
        }
        
        public ActionResult EliminarBanner(int idbanner)
        {
            consulta = "EliminarBanner";
            banner.EliminarBanner(consulta, idbanner);          
            return View("Banners");
        }

        public ActionResult AgregarBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarBanner(Banners banner)
        {
            consulta = "AgregarBanner";
            banner.AgregarBanner(consulta,banner);

            return View();
        }
    }
}