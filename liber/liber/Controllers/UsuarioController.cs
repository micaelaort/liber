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

       
        List<Libros> listLibro = new List<Libros>();
        // GET: Usuario
        public ActionResult IndexUsuario(Usuarios usuario)
        {
            consulta = "SeleccionarBanners";
            listBanner = banner.MostrarBanners(consulta);
            ViewBag.listBanner = listBanner;

            Libros libro = new Libros();
            consulta = "Libros5Usuarios";
            listLibro = libro.SeleccionarLibrosUsuario(usuario.id,consulta);
            ViewBag.listLibro = listLibro;
            return View();
        }

        public ActionResult Eventos()
        {
            return View();
        }
    }
}