using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using MySql.Data.MySqlClient;

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
        string NombreTabla;


        public void login (Usuarios oser)
        {
            DBHelper Basededatos = new DBHelper();

         
            
        }
        
        public void registrar(Usuarios ouser)
        {

        }
    }
}