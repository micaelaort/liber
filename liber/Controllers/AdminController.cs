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
            //banner.ValidaFechaExpiracion(listBanner);
            //consulta = "SeleccionarBanners";
            //listaBanner = banner.MostrarBanners(consulta);
            ViewBag.listBanner = listaBanner;
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
            banner=banner.SeleccionarBanner(consulta,idbanner);
            banner.dtFechaInicio =Convert.ToDateTime(banner.FechaInicio);
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
                    return View("ModificarBanner",banner);
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
                return View("ModificarBanner",banner);
            }


            }
             

           


         

        }



    }
