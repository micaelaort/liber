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
       
        public ActionResult Model()
        {

            return View("Model");
        }
        [HttpPost]

        public ActionResult Model(Usuarios ouser)
        {
            if (ouser.user!=" " && ouser.contraseña!=" ")
            {
                string consulta = "SeleccionarUsuario";
               Usuarios usuariobase= ouser.login(ouser,consulta);
                
                bool validarcontraseña=ouser.ValidarContraseña(ouser, usuariobase);
                bool validarusuario = ouser.ValidarUsuario(ouser,usuariobase);
                bool validaradmin = ouser.ValidarAdmin(usuariobase);
                if (validarcontraseña && validarusuario)
                {
                    //agregar if que si es admin haga tal cosa.
                  
                    if(validaradmin)
                    {
                        ViewBag.Message = "Ustes es admin";
                        return View("Admin");
                    }
                    else
                    {
                        return View("index");
                    }

                        }
                        else
                        {
                            
                            return View("Model");
                        }


            }
            else
            {
                return View("Model");
            }



          
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
        public ActionResult Admin() {

            
            return View();
        }
    }
}