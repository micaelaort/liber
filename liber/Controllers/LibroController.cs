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
        int idTitulo;
        string status;
        Usuarios usuario = new Usuarios();
        // GET: Libro
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DetallesLibro(string titulolibro)
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }
            ViewBag.usuario = usuario.id;

            //Devuelve lista comentarios
            listacomentarios = comen.ListarComentarios(titulolibro);
            ViewBag.Lista = listacomentarios;
            //Obtengo los datos del libro
            consulta = "BuscarTitulo";
            libro = libro.BuscarLibro(consulta, titulolibro);
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuaciongeneral = libro.promedio;
            ViewBag.puntuacion = comen.PuntuacionUsuario(usuario.id, libro.id);
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;

            usuario.AgregarGenero(usuario.id, libro.idgenero);
            //Obtengo el usuario
            Response.Cookies["Titulo"].Value = libro.titulo;
            Response.Cookies["Titulo"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["IdTitulo"].Value = libro.id.ToString();
            Response.Cookies["IdTitulo"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Genero"].Value = libro.idgenero.ToString();
            Response.Cookies["Genero"].Expires = DateTime.Now.AddHours(1);
            libro = libro.Opciones(libro.id, usuario.id);
            ViewBag.Guardado = libro.guardado;
            ViewBag.Leido = libro.leido;
            ViewBag.usuariocomentario = comen.idUsuarioComentario;

            return View();
        }
        [HttpPost]
        public ActionResult DetallesLibro(Comentarios comentar, string opcion)
        {
            //Obtengo los datos 
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }

            if (Request.Cookies["IdTitulo"].Value != null)
            {
                idtitulo = Request.Cookies["IdTitulo"].Value.ToString();
                titulo = Request.Cookies["Titulo"].Value.ToString();

            }
              /*Agrego la opcion y traigo todo devuelta*/
            bool estaagendado = libro.EstaAgendado(libro.id, usuario.id);

            if (estaagendado)
            {
                if (opcion == "leido")
                {
                    status = "true";
                    libro.ActualizarLeido(usuario.id, libro.id, status);
                }
                else
                {
                    if (opcion == "leidoseleccionado")
                    {
                        status = "false";
                        libro.ActualizarLeido(usuario.id, libro.id, status);
                    }
                    else
                    {
                        if (opcion == "bibloteca")
                        {
                            status = "true";
                            libro.ActualizarGuardado(usuario.id, libro.id, status);
                        }
                        else
                        {
                            status = "false";
                            libro.ActualizarGuardado(usuario.id, libro.id, status);
                        }
                    }
                }
            }

            else
            {
                /*SI NO ESTA AGENDADO EN TABLA LEIDOS*/
                if (opcion == "leido")
                {
                    status = "true";
                    libro.AñadirLeido(usuario.id, libro.id, status);
                }
                else
                {
                    if (opcion == "leidoseleccionado")
                    {
                        status = "false";
                        libro.AñadirLeido(usuario.id, libro.id, status);
                    }
                    else
                    {
                        if (opcion == "bibloteca")
                        {
                            status = "true";
                            libro.AñadirGuardado(usuario.id, libro.id, status);
                        }
                        else
                        {
                            status = "false";
                            libro.AñadirGuardado(usuario.id, libro.id, status);
                        }
                    }
                }


            }
                     
            //Agrego el comentario
            comentar.AgregarComentario(idtitulo, usuario.id, comentar.Comentario);
            //Devuelve lista comentarios
            listacomentarios = new List<Comentarios>();
            listacomentarios = comen.ListarComentarios(titulo);
            ViewBag.Lista = listacomentarios;
            consulta = "BuscarTitulo";
            libro = new Libros();
            libro = libro.BuscarLibro(consulta, titulo);
            ViewBag.id = libro.id;
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuaciongeneral = libro.promedio;
            ViewBag.puntuacion = comen.PuntuacionUsuario(usuario.id, libro.id);
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;
            libro = libro.Opciones(Convert.ToInt32(idtitulo), usuario.id);
            ViewBag.Guardado = libro.guardado;
            ViewBag.Leido = libro.leido;

            return View(comen);
        }

        public ActionResult Calificar(Comentarios com)
        {

            if (Request.Cookies["IdTitulo"].Value != null)
            {

                idTitulo = Convert.ToInt32(Request.Cookies["IdTitulo"].Value.ToString());
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);

            }
            //Recibo la punt, la mando a la base de datos y saco promedio
            comen.EliminarPuntuacion(idTitulo, usuario.id);
            comen.AgregarPuntuacion(com.puntuacionusuario, idTitulo, usuario.id);

            //Obtengo los datos 
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }

            if (Request.Cookies["IdTitulo"].Value != null)
            {
                idtitulo = Request.Cookies["IdTitulo"].Value.ToString();
                titulo = Request.Cookies["Titulo"].Value.ToString();

            }

            //Devuelve lista comentarios
            listacomentarios = new List<Comentarios>();
            listacomentarios = comen.ListarComentarios(titulo);
            ViewBag.Lista = listacomentarios;
            consulta = "BuscarTitulo";
            libro = new Libros();
            libro = libro.BuscarLibro(consulta, titulo);
            ViewBag.id = libro.id;
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuaciongeneral = libro.promedio;
            ViewBag.puntuacion = comen.PuntuacionUsuario(usuario.id, libro.id);
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;
            libro = libro.Opciones(Convert.ToInt32(idtitulo), usuario.id);
            ViewBag.Guardado = libro.guardado;
            ViewBag.Leido = libro.leido;

            return View("DetallesLibro");
        }
        
        public ActionResult Opciones(string opcion)
        {
            libro = new Libros();
            //Obtengo los datos 
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }

            if (Request.Cookies["IdTitulo"].Value != null)
            {
                libro.id = Convert.ToInt32(Request.Cookies["IdTitulo"].Value);
                libro.titulo = Request.Cookies["Titulo"].Value.ToString();

            }
            /*Agrego la opcion y traigo todo devuelta*/
            bool estaagendado = libro.EstaAgendado(libro.id, usuario.id);

            if (estaagendado)
            {
                if (opcion == "leido")
                {
                    status = "true";
                    libro.ActualizarLeido(usuario.id, libro.id, status);
                }
                else
                {
                    if (opcion == "loquieroleer")
                    {
                        status = "false";
                        libro.ActualizarLeido(usuario.id, libro.id, status);
                    }
                    else
                    {
                        if (opcion == "bibloteca")
                        {
                            status = "true";
                            libro.ActualizarGuardado(usuario.id, libro.id, status);
                        }
                        else
                        {
                            status = "false";
                            libro.ActualizarGuardado(usuario.id, libro.id, status);
                        }
                    }
                }
            }

            else
            {
                /*SI NO ESTA AGENDADO EN TABLA LEIDOS*/
                if (opcion == "leido")
                {
                    status = "true";
                    libro.AñadirLeido(usuario.id, libro.id, status);
                }
                else
                {
                    if (opcion == "leidoseleccionado")
                    {
                        status = "false";
                        libro.AñadirLeido(usuario.id, libro.id, status);
                    }
                    else
                    {
                        if (opcion == "bibloteca")
                        {
                            status = "true";
                            libro.AñadirGuardado(usuario.id, libro.id, status);
                        }
                        else
                        {
                            status = "false";
                            libro.AñadirGuardado(usuario.id, libro.id, status);
                        }
                    }
                }


            }
                     
                     
            //Devuelve lista comentarios
            listacomentarios = new List<Comentarios>();
            listacomentarios = comen.ListarComentarios(libro.titulo);
            ViewBag.Lista = listacomentarios;
            consulta = "BuscarTitulo";
            
            libro = libro.BuscarLibro(consulta, libro.titulo);
            ViewBag.id = libro.id;
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuaciongeneral = libro.promedio;
            ViewBag.puntuacion = comen.PuntuacionUsuario(usuario.id, libro.id);
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;
            libro = libro.Opciones(libro.id, usuario.id);
            ViewBag.Guardado = libro.guardado;
            ViewBag.Leido = libro.leido;
            return View("DetallesLibro");
        }
        public ActionResult ListadoLibroEncontrado(Libros libro)
        {
            ViewBag.ingresado = libro.ingresado;
           ViewBag.lista=libro.TraerSeleccionado(libro.ingresado);
            
                       //Aca va todo lo encontrado-
            return View();
        }

        public ActionResult EliminarComentario(int id)
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }
            if (Request.Cookies["IdTitulo"].Value != null)
            {
                idtitulo = Request.Cookies["IdTitulo"].Value.ToString();
                titulo = Request.Cookies["Titulo"].Value.ToString();

            }
            comen.EliminarComentario(id, usuario.id);
            listacomentarios = new List<Comentarios>();
            listacomentarios = comen.ListarComentarios(titulo);
            ViewBag.Lista = listacomentarios;
            consulta = "BuscarTitulo";
            libro = new Libros();
            libro = libro.BuscarLibro(consulta, titulo);
            ViewBag.id = libro.id;
            ViewBag.titulo = libro.titulo;
            ViewBag.puntuaciongeneral =libro.promedio; 
            ViewBag.puntuacion =comen.PuntuacionUsuario(usuario.id,libro.id); 
            ViewBag.autor = libro.autor;
            ViewBag.genero = libro.genero;
            ViewBag.sinopsis = libro.sinopsis;
            ViewBag.imagen = libro.imagen;
            libro = libro.Opciones(Convert.ToInt32(idtitulo), usuario.id);
            ViewBag.Guardado = libro.guardado;
            ViewBag.Leido = libro.leido;

            return View("DetallesLibro",libro.titulo);
        }
        public ActionResult ListadoLibroNoEncontrado()
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
                    listLibro = libro.SeleccionarMejor(consulta);
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
        public ActionResult Parametros(string parametro)
        {
            //Si no lo encontro deberia mostrarse opciones similares  

            string h = parametro;
            consulta = "Buscar";
            listLibro = libro.Links(consulta, parametro);
                               
                    ViewBag.libros = listLibro;
                
                     return View() ;
            }
          
        }
    }
