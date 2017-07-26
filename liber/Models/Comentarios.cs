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
        public string Libro { get; set; }
        public string Usuario { get; set; }
        string consulta;
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
    }
}