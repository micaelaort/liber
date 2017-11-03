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
    public class AdminController : Controller
    {
        string consulta;
        Banners banner = new Banners();
        List<Banners> listBanner = new List<Models.Banners>();
        List<Banners> listaBanner = new List<Models.Banners>();
        Eventos evento = new Eventos();
        List<Eventos> listaEventos = new List<Eventos>();
        List<Generos> listaGeneros = new List<Generos>();
        Generos genero = new Generos();
        Libros libros = new Libros();
        Autores autor = new Autores();
        List<Libros> listalibros = new List<Models.Libros>();
        Usuarios user = new Usuarios();
        List<Usuarios> listauser = new List<Models.Usuarios>();
        Trivias trivia = new Trivias();
        List<Trivias> listatrivia = new List<Models.Trivias>();

        // GET: Admin
        public ActionResult IndexAdmin()
        {
            return View();
        }

        public ActionResult Banners()
        {
            //Selecciona los banners, se fija que la feha final no sea menor que la actual y muestra todos los banners
            consulta = "SeleccionarBanners";
            listBanner = banner.MostrarBanners(consulta);
            ViewBag.listBanner = listBanner;

            return View();
        }

        public ActionResult EliminarBanner(int idbanner)
        {
            consulta = "EliminarBanner";
            banner.EliminarBanner(consulta, idbanner);
            consulta = "SeleccionarBanners";
            listBanner = banner.MostrarBanners(consulta);
            ViewBag.listBanner = listBanner;
            return View("Banners");
        }

        public ActionResult AgregarBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AgregarBanner(Banners banner, HttpPostedFileBase imagen)

        {
            //Valida que el resto de los datos no sean nulos    
            if (banner.Titulo != " " && banner.dtFechaFinal != Convert.ToDateTime("01/01/0001") && banner.dtFechaInicio != Convert.ToDateTime("01 / 01 / 0001") && banner.Imagen != null)
            {
                string validar = banner.ValidarFechas(banner);
                //Guarda la imagen
                string NuevaUbicacion = Server.MapPath("~/Content/Admintemp/banner/") + imagen.FileName;

                imagen.SaveAs(NuevaUbicacion);

                banner.Imagen = imagen.FileName;

                //Valida las fechas
                if (validar == "ok")
                {

                    consulta = "AgregarBanner";
                    banner.AgregarBanner(consulta, banner);
                    consulta = "SeleccionarBanners";
                    listBanner = banner.MostrarBanners(consulta);
                    ViewBag.listBanner = listBanner;
                    return View("Banners");
                }
                else
                {

                    ViewBag.mensaje = validar;
                    return View("AgregarBanner");
                }
            }


            else
            {
                //Si hay algo nulo

                return View("AgregarBanner");
            }


        }




        public ActionResult ModificarBanner(int idbanner)
        {
            consulta = "SeleccionarBanner";
            banner = banner.SeleccionarBanner(consulta, idbanner);
            banner.dtFechaInicio = Convert.ToDateTime(banner.FechaInicio);
            banner.dtFechaFinal = Convert.ToDateTime(banner.FechaFinal);

            return View("ModificarBanner", banner);
        }


        [HttpPost]

        public ActionResult ModificarBanner(Banners banner, HttpPostedFileBase imagen)
        {


            //Valida que el resto de los datos no sean nulos    
            if (banner.Titulo != " " && banner.dtFechaFinal != Convert.ToDateTime("01/01/0001") && banner.dtFechaInicio != Convert.ToDateTime("01 / 01 / 0001") && banner.Imagen != null)
            {
                string validar = banner.ValidarFechas(banner);
                //Guarda la imagen
                string NuevaUbicacion = Server.MapPath("~/Content/Admintemp/banner/") + imagen.FileName;

                imagen.SaveAs(NuevaUbicacion);

                banner.Imagen = imagen.FileName;

                //Valida las fechas
                if (validar == "ok")
                {
                    consulta = "EliminarBanner";
                    banner.EliminarBanner(consulta, banner.Id);
                    consulta = "AgregarBanner";
                    banner.AgregarBanner(consulta, banner);
                    consulta = "SeleccionarBanners";
                    listBanner = banner.MostrarBanners(consulta);
                    ViewBag.listBanner = listBanner;
                    return View("Banners");
                }
                else
                {
                    //Si hay algo invalido
                    consulta = "SeleccionarBanner";
                    banner = banner.SeleccionarBanner(consulta, banner.Id);
                    banner.dtFechaInicio = Convert.ToDateTime(banner.FechaInicio);
                    banner.dtFechaFinal = Convert.ToDateTime(banner.FechaFinal);
                    ViewBag.mensaje = validar;
                    return View("ModificarBanner", banner);
                }
            }


            else
            {
                //Si hay algo nulo
                consulta = "SeleccionarBanner";
                banner = banner.SeleccionarBanner(consulta, banner.Id);
                banner.dtFechaInicio = Convert.ToDateTime(banner.FechaInicio);
                banner.dtFechaFinal = Convert.ToDateTime(banner.FechaFinal);
                ViewBag.mensaje = "Por favor, complete todos los campos";
                return View("ModificarBanner", banner);
            }


        }


        public ActionResult VerEventos()
        {
            //Selecciona los banners, se fija que la feha final no sea menor que la actual y muestra todos los banners
            consulta = "SeleccionarEventos";
            listaEventos = evento.MostrarEvento(consulta);
            //banner.ValidaFechaExpiracion(listBanner);
            //consulta = "SeleccionarBanners";
            //listaBanner = banner.MostrarBanners(consulta);
            ViewBag.listEvento = listaEventos;
            return View();
        }

        public ActionResult AgregarEventos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarEventos(Eventos evento, HttpPostedFileBase imagen)

        {
            //Valida que el resto de los datos no sean nulos    
            if (evento.titulo != " " && evento.fecha != Convert.ToDateTime("01/01/0001") && evento.descripcion != "" && evento.imagen != null)
            {

                //Guarda la imagen
                string NuevaUbicacion = Server.MapPath("~/Content/Admintemp/evento/") + imagen.FileName;

                imagen.SaveAs(NuevaUbicacion);

                evento.imagen = imagen.FileName;

                consulta = "AgregarEvento";
                bool agregar = evento.AgregarEvento(consulta, evento);
                if (agregar)
                {
                    consulta = "SeleccionarEventos";
                    listaEventos = evento.MostrarEvento(consulta);

                    ViewBag.listEvento = listaEventos;
                    return View("VerEventos");

                }
                else
                {
                    ViewBag.Error = "Las fechas deben ser validas";
                    return View("AgregarEventos");
                }
            }
            else
            {
                ViewBag.Error = "Complete todo los campos";

                return View("AgregarEventos");
            }
        }

        public ActionResult EliminarEventos(int idevento)
        {
            consulta = "EliminarEvento";
            evento.EliminarEvento(consulta, idevento);
            consulta = "SeleccionarEventos";
            listaEventos = evento.MostrarEvento(consulta);
            ViewBag.listEvento = listaEventos;
            return View("VerEventos");
        }
        public ActionResult ModificarEvento(int idevento)
        {

            evento = evento.SeleccionarEvento(idevento);

            return View("ModificarEvento", evento);

        }

        [HttpPost]

        public ActionResult ModificarEvento(Eventos evento, HttpPostedFileBase imagen)
        {

            if (evento.titulo != " " && evento.fecha != Convert.ToDateTime("01/01/0001") && evento.descripcion != "" && evento.imagen != null)
            {

                //Guarda la imagen
                string NuevaUbicacion = Server.MapPath("~/Content/Admintemp/evento/") + imagen.FileName;

                imagen.SaveAs(NuevaUbicacion);

                evento.imagen = imagen.FileName;

                consulta = "EliminarEvento";
                evento.EliminarEvento(consulta, evento.idevento);
                consulta = "AgregarEvento";
                evento.AgregarEvento(consulta, evento);

                consulta = "SeleccionarEventos";
                listaEventos = evento.MostrarEvento(consulta);

                ViewBag.listEvento = listaEventos;
                return View("VerEventos");
            }
            else
            {

                return View("ModificarEvento", evento);
            }


        }


        public ActionResult Genero()
        {
            listaGeneros = genero.SeleccionarGeneros();
            ViewBag.listageneros = listaGeneros;
            return View();
        }
        public ActionResult AgregarGenero()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AgregarGenero(Generos gen)
        {
            if (gen.nombre != "")
            {
                //chequeo que no este repetido
                bool repetido = gen.VerificarGenero(gen.nombre);
                if (repetido)
                {
                    ViewBag.mensaje = "El género ya estaba ingresado";
                    return View("AgregarGenero");

                }
                else
                {
                    gen.AgregarGenero(gen.nombre);
                    listaGeneros = genero.SeleccionarGeneros();
                    ViewBag.listageneros = listaGeneros;
                    return View("Genero");
                }
            }
            else
            {
                //si esta vacio
                ViewBag.mensaje = "No ha ingresado un genero";
                return View("AgregarGenero");
            }


        }
        public ActionResult ModificarGenero(int idgenero)
        {
            genero = genero.SeleccionarGenero(idgenero);


            return View("ModificarGenero", genero);
        }
        [HttpPost]
        public ActionResult ModificarGenero(Generos gen)
        {
            if (gen.nombre != "")
            {
                //chequeo que no este repetido
                bool repetido = gen.VerificarGenero(gen.nombre);
                if (repetido)
                {
                    ViewBag.mensaje = "El género ya estaba ingresado";
                    return View("ModificarGenero");

                }
                else
                {
                    gen.ModificarGenero(gen);
                    listaGeneros = genero.SeleccionarGeneros();
                    ViewBag.listageneros = listaGeneros;
                    return View("Genero");
                }
            }
            else
            {
                //si esta vacio
                ViewBag.mensaje = "No ha ingresado un genero";
                return View("ModificarGenero");
            }


        }


        public ActionResult Libros()
        {
            listalibros = libros.SeleccionarLibros();
            ViewBag.lista = listalibros;
            return View();
        }

        public ActionResult AgregarLibros()
        {
            ViewBag.listagenero = genero.SeleccionarGeneros();
            ViewBag.listaautores = autor.SeleccionarAutores();
            return View();
        }
        [HttpPost]
        public ActionResult AgregarLibros(Libros lib, HttpPostedFileBase imagen)
        {

            //Guarda la imagen
            string NuevaUbicacion = Server.MapPath("~/Content/Usuariotemp/images/libros/") + imagen.FileName;

            imagen.SaveAs(NuevaUbicacion);

            lib.imagen = imagen.FileName;
            Comentarios comen = new Comentarios();
          
            libros.AgregarLibro(lib);
            int id = lib.SeleccionarIDLibro(lib.titulo);
            comen.AgregarPuntuacion(0, id, 1);
            listalibros = libros.SeleccionarLibros();
            ViewBag.lista = listalibros;
            return View("Libros");
        }
        public ActionResult ModificarLibros(int idlibro)
        {
            Response.Cookies["IdTitulo"].Value = idlibro.ToString();
            Response.Cookies["IdTitulo"].Expires = DateTime.Now.AddHours(1);
            ViewBag.listagenero = genero.SeleccionarGeneros();
            libros = libros.SeleccionarLibro(idlibro);
            return View("ModificarLibros", libros);
        }

        [HttpPost]

        public ActionResult ModificarLibros(Libros lib, HttpPostedFileBase imagen)
        {

            string NuevaUbicacion = Server.MapPath("~/Content/Usuariotemp/images/libros/") + imagen.FileName;

            imagen.SaveAs(NuevaUbicacion);

            lib.imagen = imagen.FileName;
            if (Request.Cookies["IdTitulo"].Value != null)
            {
                string id = Request.Cookies["IdTitulo"].Value.ToString();
                lib.id = Convert.ToInt32(id);
            }
            lib.ModificarAutor(lib.id, lib.autor);
            lib.ModificarGenero(lib.id, lib.genero);
            lib.ModificarImagen(lib.id, lib.imagen);
            lib.ModificarSinopsis(lib.id, lib.sinopsis);
            lib.ModificarTitulo(lib.id, lib.titulo);
            lib.ModificarVisible(lib.id, lib.visible);
            
            listalibros = libros.SeleccionarLibros();
            ViewBag.lista = listalibros;
            return View("Libros");
        }




        public ActionResult Usuarios()
        {
            listauser = user.SeleccionarUsuarios();
            ViewBag.lista = listauser;

            return View();
        }


        public ActionResult BloquearUsuario(int iduser, string bloqueado)
        {
            //Si esta bloqueado,blo=true,me lo desbloquea
            if (bloqueado == "true")
            {
                bloqueado = "";
            }
            else
            {
                bloqueado = "true";
            }
            user.Bloquear(iduser, bloqueado);
            listauser = user.SeleccionarUsuarios();
            ViewBag.lista = listauser;
            return View("Usuarios");
        }

        public ActionResult Trivias()
        {
            listatrivia = trivia.SeleccionarTrivias();
            ViewBag.lista = listatrivia;
            return View();
        }

        public ActionResult AgregarTrivia()
        {
            List<int> listacorrecto = new List<int>();
            listacorrecto.Add(1);
            listacorrecto.Add(2);
            listacorrecto.Add(3);
            ViewBag.listacorrecto = listacorrecto;

            return View();
        }

        [HttpPost]
        public ActionResult AgregarTrivia(Trivias triv, HttpPostedFileBase imagen)
        {
            if (triv.nombre!="")
            {
                string NuevaUbicacion = Server.MapPath("~/Content/Usuariotemp/images/Trivias/") + imagen.FileName;

                imagen.SaveAs(NuevaUbicacion);

                triv.imagen = imagen.FileName;
                //Si es la primera vexme agrega elnombre, si no deberia traerme el id 
                triv.AgregarNombre(triv);
                listatrivia = trivia.SeleccionarTrivias();
                ViewBag.lista = listatrivia;
           
                return View("Trivias");
            }
            else
            {
                List<int> listacorrecto = new List<int>();
                listacorrecto.Add(1);
                listacorrecto.Add(2);
                listacorrecto.Add(3);
                ViewBag.listacorrecto = listacorrecto;
                return View("AgregarTrivia", triv);
            }

        }

        public ActionResult ModificarNombre(int id)
        {

           trivia=trivia.SeleccionarTriv(id);
            return View("ModificarNombre",trivia);
        }

        [HttpPost]
        public ActionResult ModificarNombre(Trivias triv, HttpPostedFileBase imagen)
        {
            if (triv.nombre != "")
            {
                string NuevaUbicacion = Server.MapPath("~/Content/Usuariotemp/images/Trivias/") + imagen.FileName;

                imagen.SaveAs(NuevaUbicacion);

                triv.imagen = imagen.FileName;
                triv.ModificarNombre(triv);
                listatrivia = trivia.SeleccionarTrivias();
                ViewBag.lista = listatrivia;

                return View("Trivias");
            }
            else
            {
                //si esta vacio
                ViewBag.mensaje = "No ha ingresado un genero";
                return View("ModificarGenero");
            }


        }


        public ActionResult VerPreguntas(int id)
        {
            listatrivia = trivia.SeleccionarPreguntas(id);
            ViewBag.lista = listatrivia;
            Response.Cookies["idTrivia"].Value = id.ToString();
            Response.Cookies["idTrivia"].Expires = DateTime.Now.AddHours(1);
            return View();
        }

        public ActionResult EliminarPregunta(int id)
        {
            int idtriv = Convert.ToInt32(Request.Cookies["idTrivia"].Value.ToString());
            trivia.EliminarPregunta(id);
            listatrivia = trivia.SeleccionarPreguntas(idtriv);
            ViewBag.lista = listatrivia;
            return View("VerPreguntas");
        }

        public ActionResult AgregarPregunta()
        {
           
            List<int> listacorrecto = new List<int>();
            listacorrecto.Add(1);
            listacorrecto.Add(2);
            listacorrecto.Add(3);
            ViewBag.listacorrecto = listacorrecto;

            return View();
        }
        [HttpPost]
        public ActionResult AgregarPregunta(Trivias triv)
        {
            triv.AgregarPregunta(triv);
            int idtriv = Convert.ToInt32(Request.Cookies["idTrivia"].Value.ToString());
            int idpregunta = triv.SeleccionarPreguntaID(triv.pregunta);
            triv.AgregarPreguntaTrivia(idtriv, idpregunta);
            listatrivia = trivia.SeleccionarPreguntas(idtriv);
            ViewBag.lista = listatrivia;
            return View("VerPreguntas");
        }

        public ActionResult ModificarPregunta(int id)
        {
            trivia = trivia.SeleccionarPregunta(id);
            List<int> listacorrecto = new List<int>();
            listacorrecto.Add(1);
            listacorrecto.Add(2);
            listacorrecto.Add(3);
            ViewBag.listacorrecto = listacorrecto;

            return View("ModificarPregunta",trivia);
        }
       [HttpPost]
        public ActionResult ModificarPregunta(Trivias triv)
        {
            triv.ModificarPregunta(triv);
            int idtriv = Convert.ToInt32(Request.Cookies["idTrivia"].Value.ToString());
            listatrivia = trivia.SeleccionarPreguntas(idtriv);
            ViewBag.lista = listatrivia;
            return View("VerPreguntas");
        }

        public ActionResult EliminarTrivia(int id)
        {
            trivia.EliminarTrivia(id);
            listatrivia = trivia.SeleccionarTrivias();
            ViewBag.lista = listatrivia;

            return View("Trivias");
        }


    }
}


    