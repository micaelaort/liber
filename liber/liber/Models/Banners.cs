using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web.Mvc;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;


namespace liber.Models
{
    public class Banners
    {
        public int Id{get;set;}
        [Required(ErrorMessage = "Ingrese una fecha de inicio, por favor")]
        public DateTime dtFechaInicio {get;set;}

        [Required(ErrorMessage = "Ingrese una fecha final, por favor")]
        public DateTime dtFechaFinal{get;set;}

        public string   FechaInicio { get; set; }
        public string   FechaFinal { get; set; }
        [Required(ErrorMessage = "Ingrese un titulo, por favor")]
        public string Titulo{get;set;}

        [Required(ErrorMessage = "Ingrese una imagen, por favor")]
         public string Imagen{get;set;}

    
        DBHelper help = new DBHelper();
        Dictionary<int, Banners> dicBanners = new Dictionary<int, Banners>();
        List<Banners> ListBanners = new List<Banners>();

      
       

        
    

     
     


        public List<Banners> MostrarBanners(string consulta)
        {
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Banners banner     = new Banners();
                banner.Id          = Convert.ToInt32(lector["idBanner"]);
                banner.FechaInicio = lector["fechainicio"].ToString();
                banner.FechaFinal  = lector["fechafinal"].ToString();
                banner.Titulo      = lector["titulobanner"].ToString();
                banner.Imagen      = lector["imagen"].ToString();

                ListBanners.Add(banner);
            }
            return ListBanners;
        }

        public void EliminarBanner(string consulta, int idbanner)
        {
            
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PId", idbanner);
            help.miCommand.Parameters.Add(parametro1);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
        }

        public void AgregarBanner(string consulta, Banners banner)
        {
            help.AbrirConParametros(consulta);

            MySqlParameter parametro1 = new MySqlParameter("PTitulo", banner.Titulo);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PImagen", banner.Imagen);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PFechaInicio", banner.FechaInicio);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PFechaFinal", banner.FechaFinal);
            help.miCommand.Parameters.Add(parametro4);

            
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();

        }

        //deberia comparar que fechainicial no sea mayor a fechafinal
        public string ValidarFechas(Banners banner)
        {
            string mensaje;
            DateTime inicio = banner.dtFechaInicio;
            DateTime fin = banner.dtFechaFinal;
            DateTime ahora = DateTime.Today;
            //deberia tirar error si la fecha de ahora es menor a la de inicio
            if (inicio >= ahora)
            {
                    banner.FechaInicio=inicio.ToString();

                    if (inicio < fin)
                    {
                    banner.FechaFinal = fin.ToString();
                    mensaje = "ok";
                    return mensaje;
                    }
                    else
                    {
                    mensaje = "La fecha final no debe ser menor a fecha inicial";
                    return mensaje;
                }
            }
            else
            {
                mensaje = "La fecha inicial no debe ser menor que a la fecha actual";
                return mensaje;
            }
          
        }


        public Banners SeleccionarBanner(string consulta,int id)
        {
            help.AbrirConParametros(consulta);
            MySqlParameter parametro4 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro4);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            Banners banner = new Banners();

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
               
                banner.Id = Convert.ToInt32(lector["idBanner"]);
                banner.FechaInicio = lector["fechainicio"].ToString();
                banner.FechaFinal = lector["fechafinal"].ToString();
                banner.Titulo = lector["titulobanner"].ToString();
                banner.Imagen = lector["imagen"].ToString();                
            }
            return banner;
        }


        public void ModificarBanner(string consulta, Banners banner)
        {
            help.AbrirConParametros(consulta);
            MySqlParameter parametro5 = new MySqlParameter("PID", banner.Id);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro1 = new MySqlParameter("PTitulo", banner.Titulo);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PImagen", banner.Imagen);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PFechaInicio", banner.FechaInicio);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PFechaFinal", banner.FechaFinal);
            help.miCommand.Parameters.Add(parametro4);


            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();

        }


        public void ValidaFechaExpiracion(List<Banners> listadebanners)
        {
            DateTime ahora = DateTime.Today;
            for (int i = 0; i < listadebanners.Count(); i++)
            {

            
                Banners banners = listadebanners[i];
                banners.dtFechaFinal = Convert.ToDateTime(banners.FechaFinal);

                if (ahora > banners.dtFechaFinal)
                {
                    string consulta = "EliminarBanner";
                    banners.EliminarBanner(consulta,banners.Id);

                }

                              
                
            //return banner;
        }


    }


    }    
}