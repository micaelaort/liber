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
   
    public class Generos
    {
        DBHelper help = new DBHelper();
        public string nombre { get; set; }
        public int id { get; set; }
        string consulta;
        List<Generos> list6Genero = new List<Generos>();
        public void AgregarGenero(string Genero)
        {
          
            help.AbrirConParametros( "AgregarGenero");
            MySqlParameter parametro1 = new MySqlParameter("PGenero", Genero);
            help.miCommand.Parameters.Add(parametro1);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void ModificarGenero(Generos Genero)
        {

            help.AbrirConParametros("ModificarGenero");
            MySqlParameter parametro1 = new MySqlParameter("PGenero", Genero.nombre);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PID", Genero.id);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public List<Generos> SeleccionarGeneros()
        {
            help.Abrir("SeleccionarGeneros");
            MySqlDataReader lector = help.miCommand.ExecuteReader();
           
            while (lector.Read())
            {

                Generos genero = new Generos();
                genero.nombre = lector["nombre"].ToString();
                genero.id = Convert.ToInt32(lector["idGenero"]);
                list6Genero.Add(genero);
                
            }

            help.conn.Close();

            return list6Genero;
        }

        public Generos SeleccionarGenero(int id)
        {
            help.Abrir("SeleccionarGenero");
            MySqlParameter parametro2 = new MySqlParameter("PID",id);
            help.miCommand.Parameters.Add(parametro2);
          
            MySqlDataReader lector = help.miCommand.ExecuteReader();

                Generos genero = new Generos();
            while (lector.Read())
            {

                genero.nombre = lector["nombre"].ToString();
                genero.id = Convert.ToInt32(lector["idGenero"]);
               

            }

            help.conn.Close();

            return genero;
        }


        public bool VerificarGenero(string gen)
        {
            help.Abrir("SeleccionarGeneros");
            MySqlDataReader lector = help.miCommand.ExecuteReader();

            while (lector.Read())
            {

                Generos genero = new Generos();

                genero.nombre = lector["nombre"].ToString();
                if (genero.nombre.ToLower() == gen.ToLower())
                {
                    return true;
                }
            
            }

            help.conn.Close();

            return false;
        }

    }
}