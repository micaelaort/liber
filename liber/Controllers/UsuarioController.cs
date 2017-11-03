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
        Usuarios usuario=new Usuarios();
        string us;
        Eventos evento = new Eventos();
        Banners banner = new Banners();
        Libros libros = new Libros();
        Trivias trivia = new Trivias();
        List<Trivias> listatrivia = new List<Models.Trivias>();
        List<Banners> listBanner = new List<Models.Banners>();
        List<Libros> listLibro = new List<Libros>();
        List<Libros> listAutores= new List<Libros>();
        List<Eventos> ListEventos = new List<Eventos>();
        string usua, nombre, apellido, contraseña, email;
        int idus;
        // GET: Usuario
        public ActionResult IndexUsuario(Usuarios usuario)
  {
            Libros libro = new Libros();
            if (Request.Cookies["User"].Value != null)
            {
                string us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }

            consulta = "SeleccionarBanners";
            listBanner = banner.MostrarBanners(consulta);
            ViewBag.listBanner = listBanner;

            listLibro = new List<Libros>();
            List<Libros> list2 = new List<Libros>();
            int idgen=libro.EncontrarGenero(usuario.id);
            list2 = libro.BuscarGenero(idgen);
            ViewBag.listLibro = list2;
            return View();
            
        }

        public ActionResult Eventos()
        {
            ListEventos = evento.TraerEventos();
            ViewBag.lista= ListEventos;
            return View();
        }

        public ActionResult VerEvento(int idevento)
        {
            if (Request.Cookies["User"].Value != null)
            {
                string us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }

            bool agendado = evento.EstaAgendado(idevento, usuario.id);
            /*me fijo si esta*/
            if (agendado == true)
            {
                evento = evento.EventoUsuario(idevento, usuario.id);
                ViewBag.idevento = evento.idevento;
                ViewBag.titulo = evento.titulo;
                ViewBag.descripcion = evento.descripcion;
                ViewBag.imagen = evento.imagen;
                ViewBag.asistencia = evento.asistencia;
                ViewBag.fecha = evento.fecha.ToShortDateString();
                ViewBag.fechafinal = evento.fechafinal.ToShortDateString();
                ViewBag.horariocomienzo = evento.horariocomienzo.ToShortTimeString();
                ViewBag.horariofinal = evento.horariofinal.ToShortTimeString();
            }

            else
            {
                evento = evento.SeleccionarEvento(idevento);
                ViewBag.idevento     = evento.idevento;
                ViewBag.titulo      = evento.titulo;
                ViewBag.descripcion = evento.descripcion;               
                ViewBag.imagen      = evento.imagen;
                ViewBag.asistencia  = " ";
                ViewBag.fecha       = evento.fecha.ToShortDateString();
                ViewBag.fechafinal = evento.fechafinal.ToShortDateString();
                ViewBag.horariocomienzo = evento.horariocomienzo.ToShortTimeString();
                ViewBag.horariofinal = evento.horariofinal.ToShortTimeString();
            }

            return View();
        }
        public ActionResult Asistir(string idevento ,string opcion)
        {
            evento.idevento = Convert.ToInt32(idevento);
            if (Request.Cookies["User"].Value != null)
            {
                string us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }

            bool agendado = evento.EstaAgendado(evento.idevento, usuario.id);
            /*me fijo si esta*/
            if (agendado == true)
            {
                /*me fijo que opcion*/
                /*update*/
                if (opcion == "true")
                {
                    evento.ActualizarAsistencia(evento.idevento,usuario.id,opcion);
                }
                else
                {
                    evento.ActualizarAsistencia(evento.idevento, usuario.id, opcion);
                }


            }
            else
            {/*insert*/
                if (opcion == "true")
                {
                    evento.AñadirAsistencia(evento.idevento, usuario.id, opcion);
                }
                else
                {
                    evento.AñadirAsistencia(evento.idevento, usuario.id,opcion);
                }

            }

             agendado = evento.EstaAgendado(evento.idevento, usuario.id);
            /*me fijo si esta*/
            if (agendado == true)
            {
                evento = evento.EventoUsuario(evento.idevento, usuario.id);
                ViewBag.idevento = evento.idevento;
                ViewBag.titulo = evento.titulo;
                ViewBag.descripcion = evento.descripcion;
                ViewBag.imagen = evento.imagen;
                ViewBag.asistencia = evento.asistencia;
                ViewBag.fecha = evento.fecha.ToShortDateString();
                ViewBag.fechafinal = evento.fechafinal.ToShortDateString();
                ViewBag.horariocomienzo = evento.horariocomienzo.ToShortTimeString();
                ViewBag.horariofinal = evento.horariofinal.ToShortTimeString();
            }

            else
            {
                evento = evento.SeleccionarEvento(evento.idevento);
                ViewBag.idevento = evento.idevento;
                ViewBag.titulo = evento.titulo;
                ViewBag.descripcion = evento.descripcion;
                ViewBag.imagen = evento.imagen;
                ViewBag.asistencia = " ";
                ViewBag.fecha = evento.fecha.ToShortDateString();
                ViewBag.fechafinal = evento.fechafinal.ToShortDateString();
                ViewBag.horariocomienzo = evento.horariocomienzo.ToShortTimeString();
                ViewBag.horariofinal = evento.horariofinal.ToShortTimeString();
            }


            return View("VerEvento", evento.idevento);
        }

        public ActionResult MiCuenta()
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                idus = usuario.TraerUsuarios(us);
            }
            usuario.canteventos = usuario.SeleccionarCantAsistencia(idus);
            usuario.cantguardados = usuario.SeleccionarCantGuardado(idus);
            usuario.cantleidos = usuario.SeleccionarCantLeido(idus);
                       
             ViewBag.cantleido    =usuario.cantleidos ;
             ViewBag.cantguardado =usuario.cantguardados ;
             ViewBag.canteventos  =usuario.canteventos ;
          

            return View();
        }
        public ActionResult MiBiblioteca()
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }
            listLibro = libros.LibrosBiblioteca(usuario.id);
            ViewBag.listalibros = listLibro;
            return View();
        }
        public ActionResult MisLibros()
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }
            listLibro = libros.LibrosPendientes(usuario.id);
            ViewBag.listalibros = listLibro;
            return View();
        }

        public ActionResult MisEventos()
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }
            ListEventos = evento.ListadoMisEventos(usuario.id);
            ViewBag.listEvento = ListEventos;

            return View();
        }
        public ActionResult MisTrivias()
        {
            us = Request.Cookies["User"].Value.ToString();
            usuario.id = usuario.TraerUsuarios(us);
            listatrivia= trivia.MisTrivias(usuario.id);
            ViewBag.lista = listatrivia;
            return View();
        }
        public ActionResult ActualizarDatos()
        {
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
               
            }
            usuario = usuario.TraerUs(us);
            Response.Cookies["Usuario"].Value = usuario.user;
            Response.Cookies["Usuario"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Nombre"].Value = usuario.nombre;
            Response.Cookies["Nombre"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Apellido"].Value = usuario.apellido;
            Response.Cookies["Apellido"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Contraseña"].Value = usuario.contraseña;
            Response.Cookies["Contraseña"].Expires = DateTime.Now.AddHours(1);
            Response.Cookies["Email"].Value = usuario.email;
            Response.Cookies["Email"].Expires = DateTime.Now.AddHours(1);
            return View("ActualizarDatos",usuario);
        }
        [HttpPost]
        public ActionResult ActualizarDatos(Usuarios ouser)
        {
            Usuarios usermodificado = new Usuarios();
             //Traigo el id
            if (Request.Cookies["User"].Value != null)
            {
                us = Request.Cookies["User"].Value.ToString();
                usuario.id = usuario.TraerUsuarios(us);
            }
            //comparo el usuario
            if (Request.Cookies["Usuario"].Value != null)
            {
                //USER
               usua = Request.Cookies["Usuario"].Value.ToString();
                if (ouser.user!=us)
                {
                    usermodificado.user = usua;
                    usuario.ModificarNombre(usua, usuario.id);
                    Response.Cookies["User"].Value = ouser.user;
                    Response.Cookies["User"].Expires = DateTime.Now.AddHours(1);
                }
                //NOMBRE
                nombre = Request.Cookies["Nombre"].Value.ToString();
                if (ouser.nombre != nombre)
                {
                    usermodificado.nombre = nombre;
                    usuario.ModificarNombre(nombre, usuario.id);
                }
                //APELLIDO
                apellido = Request.Cookies["Apellido"].Value.ToString();
                if (ouser.apellido != apellido)
                {
                    usermodificado.apellido = apellido;
                    usuario.ModificarNombre(apellido, usuario.id);
                }
                //CONTRASEÑA
                contraseña = Request.Cookies["Contraseña"].Value.ToString();
                if (ouser.contraseña != contraseña)
                {                   
                    bool validarcontraseña = ouser.CompararContraseña(ouser);

                    if (validarcontraseña)
                    {
                        usermodificado.contraseña = contraseña;
                        usuario.ModificarNombre(contraseña, usuario.id);
                    }
                    else
                    {
                        ViewBag.mensaje = "Su contraseña y validacion no coinciden";
                    }
                    }
                //EMAIL
                email = Request.Cookies["Email"].Value.ToString();
                if (ouser.email != email)
                {
                    usermodificado.email = email;
                    usuario.ModificarNombre(email, usuario.id);
                }
            }


            return View("ActualizarDatos");
        }
        public ActionResult CerrarSesion()
        {
            return View();
        }
        public ActionResult search(Usuarios usuario)
        {
            
          
            return View();
        }

       [HttpPost]
        public ActionResult search(Libros libro)
        {
            //Hago consulta que busca el libro, devuelve un bool
            consulta = "Buscar";
            encontrado = libro.Encontrar(consulta, libro);
            if (encontrado == true)
            {
                return RedirectToAction("ListadoLibroEncontrado", "Libro", libro);
            }

            else
            {
                return RedirectToAction("ListadoLibroNoEncontrado", "Libro", libro);
            }

         
        }

        public ActionResult Trivias()
        {
            listatrivia = trivia.SeleccionarTrivias();
            ViewBag.lista = listatrivia;


            return View();
        }

        public ActionResult JugarTrivia(int id)
        {
            /*TRAER PREGUNTAS Y MANDA LA PRIMERA PREGUNTA*/
            us = Request.Cookies["User"].Value.ToString();
            usuario.id = usuario.TraerUsuarios(us);
            bool jugo = trivia.SiYaJugo(id, usuario.id);
            if (jugo == false)
            {
                listatrivia = trivia.SeleccionarPreguntas(id);
                ViewBag.lista = listatrivia[0];
                trivia.idpregunta = listatrivia[0].idpregunta;
                Response.Cookies["idTrivia"].Value = id.ToString();
                Response.Cookies["idTrivia"].Expires = DateTime.Now.AddHours(1);
                Response.Cookies["idPregunta"].Value = trivia.idpregunta.ToString();
                Response.Cookies["idPregunta"].Expires = DateTime.Now.AddHours(1);
                Response.Cookies["posicion"].Value = 0.ToString();
                Response.Cookies["posicion"].Expires = DateTime.Now.AddHours(1);
                Response.Cookies["puntuacion"].Value = 0.ToString();
                Response.Cookies["puntuacion"].Expires = DateTime.Now.AddHours(1);
                ViewBag.res1 = listatrivia[0].respuesta1;
                ViewBag.res2 = listatrivia[0].respuesta2;
                ViewBag.res3 = listatrivia[0].respuesta3;
                ViewBag.pregunta = listatrivia[0].pregunta;
                return View();
            }
            else
            {
                ViewBag.lista = trivia.Ranking(id);
                return View("Ranking");
            }
        }
       
        [HttpPost]
        public ActionResult JugarTrivia(Trivias triv)
        {   int puntuacion=Convert.ToInt32(Request.Cookies["puntuacion"].Value.ToString());
            int idpregunta=Convert.ToInt32(Request.Cookies["idPregunta"].Value.ToString());
            int posicion = Convert.ToInt32(Request.Cookies["posicion"].Value.ToString()) + 1;
            int idtriv= Convert.ToInt32(Request.Cookies["idTrivia"].Value.ToString());
            us = Request.Cookies["User"].Value.ToString();
            usuario.id = usuario.TraerUsuarios(us);
            /*CHEQUEA LA RESPUES Y MANDA OTRA PREGUNTA O DEVUELVE EL RANKING*/
            listatrivia = trivia.SeleccionarPreguntas(idtriv);
          
                if (triv.respuestauser== listatrivia[posicion-1].correcto)
                {
                puntuacion++;
                Response.Cookies["puntuacion"].Value = puntuacion.ToString();
                Response.Cookies["puntuacion"].Expires = DateTime.Now.AddHours(1);
            }
    

            if (posicion!=listatrivia.Count())
            {
                ViewBag.lista = listatrivia[posicion];
                trivia.idpregunta = listatrivia[posicion].idpregunta;
                Response.Cookies["idTrivia"].Value = idtriv.ToString();
                Response.Cookies["idTrivia"].Expires = DateTime.Now.AddHours(1);
                Response.Cookies["idPregunta"].Value = idpregunta.ToString();
                Response.Cookies["idPregunta"].Expires = DateTime.Now.AddHours(1);
                Response.Cookies["posicion"].Value = posicion.ToString();
                Response.Cookies["posicion"].Expires = DateTime.Now.AddHours(1);
                ViewBag.res1 = listatrivia[posicion].respuesta1;
                ViewBag.res2 = listatrivia[posicion].respuesta2;
                ViewBag.res3 = listatrivia[posicion].respuesta3;
                ViewBag.pregunta = listatrivia[posicion].pregunta;
                //return to jugartrvia
            }
            else
            {
                trivia.AgregarPuntuacion(puntuacion,idtriv,usuario.id);
                ViewBag.lista=trivia.Ranking(idtriv);
                return View("Ranking");

            }

            return View();
        }

        public ActionResult Ranking()
        {
          
            return View();
        }

    }
}