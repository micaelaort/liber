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
        bool encontrado;
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
            listLibro = libro.SeleccionarLibrosUsuario(usuario.id, consulta);
            ViewBag.listLibro = listLibro;
            return View();
        }

        public ActionResult Eventos()
        {
            return View();
        }
        public ActionResult search()
        {
            
            return View();
        }

       [HttpPost]
        public ActionResult search(Libros libro)
        {
            //Hago consulta que busca el libro, devuelve un bool
            consulta = "buscar";
            encontrado = libro.Encontrar(consulta, libro);
            if (encontrado == true)
            {
                return RedirectToAction(" ListadoLibroEncontrado", "Libro", libro);
            }

            else
            {
                return RedirectToAction(" ListadoLibroNoEncontrado", "Libro", libro);
            }

         
        }
    }
}