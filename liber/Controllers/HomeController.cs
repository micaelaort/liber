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
            if (ouser.user != " " && ouser.contraseña != " ")
            {
                string consulta = "SeleccionarUsuarioLogin";Usuarios usuariobase = ouser.login(ouser, consulta);
                if (usuariobase.bloqueado != "true")
                {

                    
                    bool validarcontraseña = ouser.ValidarContraseña(ouser, usuariobase);
                    bool validarusuario = ouser.ValidarUsuario(ouser, usuariobase);
                    bool validaradmin = ouser.ValidarAdmin(usuariobase);
                    if (validarcontraseña && validarusuario)
                    {
                        //agregar if que si es admin haga tal cosa.

                        if (validaradmin)
                        {
                            ViewBag.Message = "Usted es admin";
                            return RedirectToAction("IndexAdmin", "Admin");
                        }
                        else
                        {

                            Response.Cookies["User"].Value = ouser.user;
                            Response.Cookies["User"].Expires = DateTime.Now.AddHours(1);

                            return RedirectToAction("IndexUsuario", "Usuario", usuariobase);
                        }

                    }
                    else
                    {
                        ViewBag.mensaje = " El usuario o contraseña es invalido";
                        return View("Model");
                    }



                }
                else
                {ViewBag.mensaje = "Usted ha sido bloquedo";

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
        public ActionResult Registrar(Usuarios ouser)
        {
            string consulta ="AgregarUsuario";
            
            if (ModelState.IsValid)
            {
                bool validarcontraseña = ouser.CompararContraseña(ouser);

                            if (validarcontraseña)
                            {
                                ouser.registrar(ouser, consulta);
                                return View("index");
                            }
                            else
                            {
                    ViewBag.mensaje = "Su contraseña y validacion no coinciden";
                                 return View("Registrar");

                            }
               

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