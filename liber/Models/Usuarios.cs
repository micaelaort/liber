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

        [Required(ErrorMessage = "Ingrese un nombre, por favor")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "Ingrese un email, por favor")]
        public string email { get; set; }

        [Required(ErrorMessage = "Ingrese la validacón de la contraseña, por favor")]
        public string confirmaciondecontraseña { get; set; }

        public int id { get; set; }
        public string bloqueado { get; set; }
        public int cantleidos { get; set; }
        public int cantguardados { get; set; }
        public int canteventos { get; set; }

        public Usuarios UsuarioBaseDatos;
        public string Administrador;
        DBHelper help = new DBHelper();
        string consulta;
        List<Usuarios> lista = new List<Usuarios>();
        public Usuarios login(Usuarios user, string consulta)
        {

            help.Abrir(consulta);


            MySqlParameter parametro1 = new MySqlParameter("PUser", user.user);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PPassword", user.contraseña);
            help.miCommand.Parameters.Add(parametro2);

            MySqlDataReader lector = help.miCommand.ExecuteReader();

            if (lector.Read())
            {
                UsuarioBaseDatos = new Usuarios();
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.user = lector["usuario"].ToString();
                UsuarioBaseDatos.contraseña = lector["password"].ToString();
                UsuarioBaseDatos.Administrador = (lector["admin"].ToString());
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.bloqueado = lector["bloqueado"].ToString();
            }
            else
            {
                UsuarioBaseDatos = new Usuarios();
                UsuarioBaseDatos.user = "";
                UsuarioBaseDatos.contraseña = "";
                UsuarioBaseDatos.Administrador = "";
                UsuarioBaseDatos.id = 0;
            }
            help.conn.Close();
            return UsuarioBaseDatos;


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

            MySqlParameter parametro5 = new MySqlParameter("PPassword", ouser.contraseña);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro6 = new MySqlParameter("PAdmin", "false");
            help.miCommand.Parameters.Add(parametro6);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }
        public Boolean ValidarContraseña(Usuarios ouser, Usuarios ouser2)
        {
            if (ouser2.contraseña == ouser.contraseña && ouser2.contraseña != " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean ValidarUsuario(Usuarios ouser, Usuarios ouser2)
        {
            if (ouser2.user == ouser.user && ouser2.user != " ")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean ValidarAdmin(Usuarios ouser)
        {
            //Si return true es admin
            if (ouser.Administrador == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Boolean CompararContraseña(Usuarios ouser)
        {
            if (ouser.contraseña == ouser.confirmaciondecontraseña)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public int TraerUsuarios(string user)
        {
            string consulta = "TraerUsuario";
            help.Abrir(consulta);


            MySqlParameter parametro1 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro1);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            UsuarioBaseDatos = new Usuarios();
            while (lector.Read())
            {

                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);


            }
            help.conn.Close();
            return UsuarioBaseDatos.id;
        }
        public Usuarios TraerUs(string user)
        {
            string consulta = "TraerUsuario";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro1 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro1);

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            UsuarioBaseDatos = new Usuarios();
            while (lector.Read())
            {
                UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.nombre = lector["nombre"].ToString();
                UsuarioBaseDatos.user = lector["usuario"].ToString();
                UsuarioBaseDatos.apellido = lector["apellido"].ToString();
                UsuarioBaseDatos.email = (lector["email"].ToString());
                UsuarioBaseDatos.contraseña = lector["password"].ToString();
            }
            help.conn.Close();
            return UsuarioBaseDatos;
        }

        public void AgregarGenero(int usuario, int genero)
        {
            string consulta = "AgregarRecomendado";

            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);


            MySqlParameter parametro1 = new MySqlParameter("PUser", usuario);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PGenero", genero);
            help.miCommand.Parameters.Add(parametro2);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        //lo guardo todo en una cookie y despues comparo si cambió algo
        public void ModificarNombre(String nombre, int id)
        {
            consulta = "ModificarNombre";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PNombre", nombre);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro10 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro10);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();

        }

        public void ModificarApellido(string apellido, int id)
        {
            consulta = "ModificarApellido";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro10 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro10);
            MySqlParameter parametro2 = new MySqlParameter("PApellido", apellido);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public void ModificarUsuario(string usuario, int id)
        {
            consulta = "ModificarUsuario";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro10 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro10);
            MySqlParameter parametro3 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro3);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public void ModificarEmail(string email, int id)
        {
            consulta = "ModificarEmail";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro10 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro10);
            MySqlParameter parametro4 = new MySqlParameter("PEmail", email);
            help.miCommand.Parameters.Add(parametro4);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public void ModificarContraseña(string contraseña, int id)
        {
            consulta = "ModificarPassword";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro10 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro10);
            MySqlParameter parametro5 = new MySqlParameter("PPassword", contraseña);
            help.miCommand.Parameters.Add(parametro5);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public int SeleccionarCantLeido(int user)
        {
            string consulta = "SeleccionarCantLeido";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro1 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro1);

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            UsuarioBaseDatos = new Usuarios();
            while (lector.Read())
            {
                UsuarioBaseDatos.cantleidos = Convert.ToInt32(lector["cantleido"]);

            }
            help.conn.Close();
            return UsuarioBaseDatos.cantleidos;

        }
        public int SeleccionarCantGuardado(int user)
        {
            string consulta = "SeleccionarCantGuardado";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro1 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro1);

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            UsuarioBaseDatos = new Usuarios();
            while (lector.Read())
            {
                UsuarioBaseDatos.cantguardados = Convert.ToInt32(lector["cantguardado"]);

            }
            help.conn.Close();
            return UsuarioBaseDatos.cantguardados;

        }
        public int SeleccionarCantAsistencia(int user)
        {
            string consulta = "SeleccionarCantAsistencia";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro1 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro1);

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            UsuarioBaseDatos = new Usuarios();
            while (lector.Read())
            {
                UsuarioBaseDatos.canteventos = Convert.ToInt32(lector["cantasistencia"]);
            }
            help.conn.Close();
            return UsuarioBaseDatos.canteventos;

        }

        public List<Usuarios> SeleccionarUsuarios()
        {
            string consulta = "SeleccionarUsuarios";
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
         
            while (lector.Read())
            {
                 UsuarioBaseDatos = new Usuarios();
                 UsuarioBaseDatos.id = Convert.ToInt32(lector["idUsuario"]);
                UsuarioBaseDatos.user = lector["usuario"].ToString();
                UsuarioBaseDatos.bloqueado= lector["bloqueado"].ToString();

                lista.Add(UsuarioBaseDatos);
            }
            help.conn.Close();
            return lista;
        }
        public void Bloquear(int id,string bloquear)
        {
            consulta = "ActualizarBloqueado";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro10 = new MySqlParameter("PUser", id);
            help.miCommand.Parameters.Add(parametro10);
            MySqlParameter parametro2 = new MySqlParameter("PBloqueado", bloquear);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }


    }
}