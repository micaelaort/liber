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
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Index(Usuarios Ousuariorecibido)
        {
            bool login;
            
            string usuario=Ousuariorecibido.user;
            string contra = Ousuariorecibido.contraseña;
            string cons = "SeleccionarUsuario";
            login=Ousuariorecibido.login(Ousuariorecibido,cons);

            //

                return View("Index");
            
            
          
            
        }
        public ActionResult Model()
        {

            return View("Model");
        }
        [HttpPost]

        public ActionResult Model(Usuarios oser)
        {
            if (ModelState.IsValid)
            {
                return View("index");

            }
            else
            {
                return View("Model");
            }



          
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
            string consulta ="AgregarUsuario";
            
            if (ModelState.IsValid)
            {
                ousuario.registrar(ousuario, consulta);
                return View("index");

            }
            else
            {
                return View("Registrar");
            }
           
        }
    }
}