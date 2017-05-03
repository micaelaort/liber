﻿using System;
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
    public class Usuarios
    {
        [Required(ErrorMessage = "Ingrese un usuario, por favor")]
        public string user { get; set; }
        
        [Required(ErrorMessage = "Ingrese una contraseña, por favor")]
        public string contraseña { get; set; }

        [Required(ErrorMessage = "Ingrese un apellido, por favor")]
        public string apellido { get; set; }

        [Required(ErrorMessage ="Ingrese un nombre, por favor")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un email, por favor")]
        public string email{ get; set; }

        [Required(ErrorMessage = "Ingrese la validacón de la contraseña, por favor")]
        public string confirmaciondecontraseña{ get; set; }

        public string modal;      

      


        public bool login (Usuarios user,string consulta)
        {
            DBHelper help = new DBHelper();
            help.Abrir();
            
            help.miCommand.CommandText = consulta;
            MySqlParameter parametro1 = new MySqlParameter("PUser", user.user);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PContraseña", user.contraseña);
            help.miCommand.Parameters.Add(parametro2);

            MySqlDataReader lector = help.miCommand.ExecuteReader();

            if (lector.Read())
            {
                return true;
            }
            help.conn.Close();
            return false;


        }
     
        public void registrar(Usuarios ouser, string consulta)
        {
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);

         
            MySqlParameter parametro1 = new MySqlParameter("PNombre", ouser.nombre);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PApellido", ouser.apellido);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PUser", ouser.user);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PEmail", ouser.email);
            help.miCommand.Parameters.Add(parametro4);

            MySqlParameter parametro5 = new MySqlParameter("PContraseña", ouser.contraseña);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro6 = new MySqlParameter("PAdmin", "0");
            help.miCommand.Parameters.Add(parametro6);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();



        }
    }
}