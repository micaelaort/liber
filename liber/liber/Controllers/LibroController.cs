using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace liber.Controllers
{
    public class LibroController : Controller
    {
        // GET: Libro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetallesLibro()
        {
            return View();
        }

        public ActionResult ListadoLibroEncontrado()
        {
            //Aca va todo lo encontrado-
            return View();
        }


        public ActionResult ListadoLibroNoEncontrado()
        {
            ViewBag.listlibro="";
            //Si no lo encontro deberia mostrarse opciones similares                                                                                                                                                                     
            return View();
        }
        public ActionResult Vermas(string vermas)
        {
            //Si no lo encontro deberia mostrarse opciones similares  
            ViewBag.titulo = vermas;                                                                                                                                                                   
            return View();
        }
    }
}