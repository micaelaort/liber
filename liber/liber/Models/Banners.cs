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
        public DateTime FechaInicio {get;set;}
        public DateTime FechaFinal{get;set;}
        public string Titulo{get;set;}
        public string Imagen{get;set;}
        DBHelper help = new DBHelper();
        Dictionary<int, Banners> dicBanners = new Dictionary<int, Banners>();


        public Dictionary<int,Banners> MostrarBanners(string consulta)
        {
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Banners banner     = new Banners();
                banner.Id          = Convert.ToInt32(lector["idBanner"]);
                banner.FechaInicio = Convert.ToDateTime(lector["fechainicio"]);
                banner.FechaFinal  = Convert.ToDateTime(lector["fechafinal"]);
                banner.Titulo      = lector["titulo"].ToString();
                banner.Imagen      = lector["imagen"].ToString();

                dicBanners.Add(banner.Id, banner);
            }
            return dicBanners;
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
        
               
     }    
}