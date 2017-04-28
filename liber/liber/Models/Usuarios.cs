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

      
        


        public void login (Usuarios user)
        {
            DBHelper Basededatos = new DBHelper();
            string consulta = "Consulta01";
            Basededatos.Abrir(consulta);
            MySqlDataReader lector=Basededatos.Comando.ExecuteReader();
            while (lector.Read())
            {
                Usuarios ouser = new Usuarios();
                ouser.user       = lector["user"].ToString();
                ouser.contraseña = lector["contraseña"].ToString();
            }
           Basededatos.miConn.Close();

        }
        
        public void registrar(Usuarios ouser)
        {

        }
    }
}