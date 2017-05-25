using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace liber.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult IndexUsuario()
        {
            return View();
        }

        public ActionResult Eventos()
        {
            return View();
        }
    }
}