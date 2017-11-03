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
    public class Comentarios
    {
        public string Comentario { get; set; }
        public int idComentario { get; set; }
        public int idUsuarioComentario { get; set; }
        public string Libro { get; set; }
        public string Usuario { get; set; }
        string consulta;
        public int bibloteca { get; set; }
        public int puntuacionusuario { get; set; }
        public int Promedio;
        DBHelper help = new DBHelper();
        List<Comentarios> ListaComentarios = new List<Comentarios>(); 
        public List<Comentarios> ListarComentarios(string titulo)
        {     
            consulta = "SeleccionarComentarios";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PTitulo", titulo);
            help.miCommand.Parameters.Add(parametro1);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
          
            while (lector.Read())
            {
                Comentarios comentario = new Comentarios();
                comentario.idComentario= Convert.ToInt32(lector["idComentario"]);
                comentario.Comentario= lector["Comentario"].ToString();
                comentario.Libro= lector["titulo"].ToString();
                comentario.Usuario = lector["usuario"].ToString();
                ListaComentarios.Add(comentario);
            }
            help.conn.Close();
            return ListaComentarios;
        }

        public void AgregarComentario(string titulo, int id, string comentario)
        {
            int idlibro = Convert.ToInt32(titulo);
            consulta = "AgregarComentario";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PIdLibro",idlibro);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PIdUsuario", id);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PComentario", comentario);
            help.miCommand.Parameters.Add(parametro3);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
            }

        public void AgregarPuntuacion(double puntuacion,int idlibro, int iduser)
        {
            consulta = "AgregarPuntuacion";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUsuario", iduser);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PPuntuacion", puntuacion);
            help.miCommand.Parameters.Add(parametro3);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public void EliminarPuntuacion( int idlibro, int iduser)
        {
            consulta = "EliminarPuntuacion";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public void EliminarComentario(int idcomentario, int iduser)
        {
            consulta = "EliminarComentario";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PComentario", idcomentario);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public int PuntuacionUsuario(int iduser, int idlibro)
        { Comentarios comentario = new Comentarios();
            consulta = "SeleccionarPuntuacionUser";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro2);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                
                comentario.puntuacionusuario = Convert.ToInt32(lector["PuntuacionUser"]);
             }
        
            help.conn.Close();
            return comentario.puntuacionusuario;
        }
    }
}