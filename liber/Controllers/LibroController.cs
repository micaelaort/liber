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
        Libros libro = new Libros();
        Comentarios comen = new Comentarios();
        List<Comentarios> listacomentarios = new List<Comentarios>();
        string consulta;
        string us;
        string idtitulo;
        string titulo;
        string id;
        Usuarios usuario= new Usuarios();
        // GET: Libro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetallesLibro(string titulolibro)
        {
            
            //Devuelve lista comentarios
            listacomentarios = comen.ListarComentarios(titulolibro);
            ViewBag.Lista = listacomentarios;
            //Obtengo los datos del libro
            consulta = "BuscarTitulo";
            libro = libro.BuscarLibro(consulta, titulolibro);
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuacion = libro.puntacion;
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;
            //Obtengo el usuario
            Response.Cookies["Titulo"].Value = libro.titulo;
            Response.Cookies["Titulo"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["IdTitulo"].Value = libro.id.ToString();
            Response.Cookies["IdTitulo"].Expires = DateTime.Now.AddHours(1);
            switch (libro.puntacion.ToString())
            {
                case "1":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "2":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "3":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "4":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "5":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    break;
            }

            return View();
        }
        [HttpPost]
        public ActionResult DetallesLibro(Comentarios comentar)
        {
            //Obtengo los datos 
            if (Request.Cookies["User"].Value != null)
            {
               us = Request.Cookies["User"].Value.ToString();
               usuario.id = usuario.TraerUsuarios(us);
            }

            if (Request.Cookies["IdTitulo"].Value != null)
            {
              idtitulo= Request.Cookies["IdTitulo"].Value.ToString();
              titulo = Request.Cookies["Titulo"].Value.ToString();

            }
            //Agrego el comentario
             comentar.AgregarComentario(idtitulo, usuario.id, comentar.Comentario);
            //Devuelve lista comentarios
            listacomentarios = new List<Comentarios>();
            listacomentarios= comen.ListarComentarios(titulo);
            ViewBag.Lista = listacomentarios;
            consulta = "BuscarTitulo";
            libro = libro.BuscarLibro(consulta, titulo);
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuacion = libro.puntacion;
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;
            //Obtengo el usuario
            Response.Cookies["Titulo"].Value = libro.id.ToString();
            Response.Cookies["Titulo"].Expires = DateTime.Now.AddHours(1);
            switch (libro.puntacion.ToString())
            {
                case "1":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "2":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "3":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "4":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellablanca.png");
                    break;
                case "5":
                    ViewData["estrella1"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella2"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella3"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella4"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    ViewData["estrella5"] = Url.Content("~/Content/Usuariotemp/images/libros/estrellacolor.png");
                    break;
            }
            
            return View();
        }

        public ActionResult ListadoLibroEncontrado(Libros libro)
        {
            ViewBag.ingresado = libro.ingresado;
           ViewBag.lista=libro.TraerSeleccionado(libro.ingresado);
            
                       //Aca va todo lo encontrado-
            return View();
        }


        public ActionResult ListadoLibroNoEncontrado(Libros libro)
        {
            ViewBag.ingresado = libro.ingresado;
            List<Libros> listLibro1 = new List<Libros>();
            List<Libros> listLibro2 = new List<Libros>();
            List<Libros> listLibro3 = new List<Libros>();
            consulta = "SeleccionarMejor";
            listLibro1 = libro.SeleccionarMejor(consulta);
            ViewBag.mejor = listLibro1;
            consulta = "SeleccionarGenero1";
            listLibro2 = libro.SeleccionarGenero1(consulta);
            ViewBag.genero1 = listLibro2;
            consulta = "SeleccionarGenero2";
            listLibro3 = libro.SeleccionarGenero2(consulta);
            ViewBag.genero2 = listLibro3;

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