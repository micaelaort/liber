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
    public class Libros
    {
        public string titulo {get;set;}
        public string autor {get;set;}
        public string sinopsis {get;set;}
        public string editorial {get;set;}
        public string reseña{get;set;}
        public int    puntacion {get;set;}
        public string genero { get; set; }
        public string imagen { get; set; }
        public int     id { get; set; }


        DBHelper help = new DBHelper();
        Dictionary<int, Libros> dicLibros = new Dictionary<int, Libros>();
        List<Libros> listLibro = new List<Libros>();
        public List<Libros> SeleccionarLibrosUsuario(string usuarionombre, string consulta)
        {

            help.AbrirConParametros(consulta);
            /*Le mando el nombreuser y me devuelve 5 o menos libros del usario*/
            MySqlParameter parametro1 = new MySqlParameter("PNombre", usuarionombre);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Libros librorecibido = new Libros();
                librorecibido.id = Convert.ToInt32(lector["idLibro"]);
                librorecibido.titulo    =  lector["titulo"].ToString();
                librorecibido.autor     =  lector["autor"].ToString();
                librorecibido.imagen    =  lector["tapa"].ToString();
                librorecibido.sinopsis  =  lector["sinopsis"].ToString();           
                librorecibido.genero    =  lector["genero"].ToString();
                librorecibido.puntacion =  Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);


                       }

            return listLibro;
        }

    }
}