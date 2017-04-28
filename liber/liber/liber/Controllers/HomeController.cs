using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.OleDb;
using liber.Models;
using System.IO;

namespace liber.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(Usuarios Ousuariorecibido)
        {
            
            string usuario=Ousuariorecibido.user;
            string contra = Ousuariorecibido.contraseña;
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Registrar()
        {
          
            

            return View();
        }

        [HttpPost]
        public ActionResult Registrar(Usuarios ousuario)
        {
            if (ModelState.IsValid)
            {
                return View("index");

            }
            else
            {
                return View("Registrar");
            }
           
        }
    }
}