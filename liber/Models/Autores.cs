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
    public class Autores
    {
        public int idautor { get; set; }
        public string nombre{ get; set; }
        List<Autores> listautores = new List<Autores>();
        DBHelper help = new DBHelper();
        string consulta;

        public List<Autores> SeleccionarAutores()
        {

            consulta = "SeleccionarAutores";
            help.AbrirConParametros(consulta);

            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
         
            while (lector.Read())
            {
                Autores autor = new Autores();

               autor.idautor = Convert.ToInt32(lector["idAutor"]);
               autor.nombre = lector["nombre"].ToString();
               

                listautores.Add(autor);
            }

            help.conn.Close();
            return listautores;

        }

    }
}