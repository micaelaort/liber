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
    public class LibroController : Controller
    {
        List<Libros> listLibro = new List<Libros>();
        Libros libro= new Libros();
        string consulta;
        // GET: Libro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetallesLibro()
        {
            ViewBag.titulo = "hola";
            ViewBag.autor = "afgsf";
            return View();
        }


        public ActionResult ListadoLibroEncontrado()
        {
            //Aca va todo lo encontrado-
            return View();
        }


        public ActionResult ListadoLibroNoEncontrado()
        {
            consulta = "SeleccionarMejor";
            listLibro = libro.SeleccionarMejor(consulta);
            ViewBag.mejor = listLibro;
            consulta = "SeleccionarGenero1";
            listLibro = libro.SeleccionarMejor(consulta);
            ViewBag.genero1 = listLibro;
            consulta = "SeleccionarGenero2";
            listLibro = libro.SeleccionarMejor(consulta);
            ViewBag.genero2 = listLibro;

            //Si no lo encontro deberia mostrarse opciones similares                                                                                                                                                                     
            return View();
        }
        public ActionResult Vermas(string vermas)
        {
            //Si no lo encontro deberia mostrarse opciones similares  
            ViewBag.titulo = vermas;
            switch (vermas)
            {
                case "Mejor":
                    consulta = "SeleccionarMejor";
                   listLibro=libro.SeleccionarMejor(consulta);
                    ViewBag.libros = listLibro;
                    break;
                case "Genero1":
                    consulta = "SeleccionarGenero1";
                    listLibro = libro.SeleccionarMejor(consulta);
                    ViewBag.libros = listLibro;
                    break;
                case "Genero2":
                    consulta = "SeleccionarGenero2";
                    listLibro = libro.SeleccionarMejor(consulta);
                    ViewBag.libros = listLibro;
                    break;
              



            }                                                                                                                                                                 
            return View();
        }
    }
}