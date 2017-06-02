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
    public class UsuarioController : Controller
    {
        string consulta;
        Banners banner = new Banners();
        List<Banners> listBanner = new List<Models.Banners>();

        Libros libro = new Libros();
        List<Libros> listLibro = new List<Libros>();
        // GET: Usuario
        public ActionResult IndexUsuario(string usuarionombre)
        {
            consulta = "SeleccionarBanners";
            listBanner = banner.MostrarBanners(consulta);
            ViewBag.listBanner = listBanner;

            consulta = "Libros5Usuarios";
            listLibro = libro.SeleccionarLibrosUsuario(usuarionombre,consulta);
            ViewBag.listLibro = listLibro;
            return View();
        }

        public ActionResult Eventos()
        {
            return View();
        }
    }
}