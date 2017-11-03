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
    public class Trivias
    {
        public int id { get; set; }
        public string imagen { get; set; }
        public int respuestauser { get; set; }
        public string user { get; set; }
        public int idpregunta { get; set; }
        public int puntuacion { get; set; }
        [Required(ErrorMessage = "Ingrese nombre, por favor")]
        public string nombre { get; set; }
        [Required(ErrorMessage = "Ingrese pregunta, por favor")]
        public string pregunta { get; set; }
        [Required(ErrorMessage = "Ingrese respuesta, por favor")]
        public string respuesta1 { get; set; }
        [Required(ErrorMessage = "Ingrese respuesta, por favor")]
        public string respuesta2 { get; set; }
        [Required(ErrorMessage = "Ingrese respuesta, por favor")]
        public string respuesta3 { get; set; }
        public int correcto { get; set; }
        DBHelper help;
     
        public void AgregarPregunta(Trivias trivia)
        {
            help = new DBHelper();
            help.AbrirConParametros("AgregarPregunta");

            MySqlParameter parametro2 = new MySqlParameter("PPregunta", trivia.pregunta);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PRespuesta1", trivia.respuesta1);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PRespuesta2", trivia.respuesta2);
            help.miCommand.Parameters.Add(parametro4);

            MySqlParameter parametro5 = new MySqlParameter("PRespuesta3", trivia.respuesta3);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro6 = new MySqlParameter("PCorrecto", trivia.correcto);
            help.miCommand.Parameters.Add(parametro6);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }
        public void AgregarNombre(Trivias trivia)
        {
            help = new DBHelper();
            help.AbrirConParametros("AgregarNombre");
            MySqlParameter parametro1 = new MySqlParameter("PNombre", trivia.nombre);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PImagen", trivia.imagen);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }

        public int SeleccionarTrivia(String nombre)
        {
            help = new DBHelper();
            string consulta = "SeleccionarTriviaID";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PNombre",nombre);
            help.miCommand.Parameters.Add(parametro1);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            Trivias triv = new Trivias();
            while (lector.Read())
            {
                triv.id = Convert.ToInt32(lector["idTrivia"]);
            }
            help.conn.Close();
            return triv.id;
        }

        public List<Trivias> SeleccionarTrivias()
        {
            help = new DBHelper();
            List<Trivias> lista = new List<Trivias>();
            string consulta = "SeleccionarTrivias";
            help.AbrirConParametros(consulta);
            

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            
            while (lector.Read())
            {
                Trivias triv = new Trivias();
                triv.id = Convert.ToInt32(lector["idTrivia"]);
                triv.nombre = lector["nombre"].ToString();
                triv.imagen = lector["imagen"].ToString();

                lista.Add(triv); 
            }
            help.conn.Close();
            return lista;
        }

        public int SeleccionarPreguntaID(String pregunta)
        {
            help = new DBHelper();
            string consulta = "SeleccionarPreguntaID";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PPregunta", pregunta);
            help.miCommand.Parameters.Add(parametro1);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            Trivias triv = new Trivias();
            while (lector.Read())
            {
                triv.idpregunta = Convert.ToInt32(lector["idPregunta"]);
              
            }
            help.conn.Close();
            return triv.idpregunta;
        }
        public void AgregarPreguntaTrivia(int trivia,int pregunta)
        {
            help = new DBHelper();
            help.AbrirConParametros("AgregarPreguntaTrivia");
            MySqlParameter parametro1 = new MySqlParameter("PTrivia", trivia);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PPregunta", pregunta);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }
        public void ModificarNombre(Trivias trivia)
        {
            help = new DBHelper();
            help.AbrirConParametros("ModificarNombreTrivia");
            MySqlParameter parametro1 = new MySqlParameter("PNombre", trivia.nombre);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PID", trivia.id);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PImagen", trivia.imagen);
            help.miCommand.Parameters.Add(parametro3);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }

        public List<Trivias> SeleccionarPreguntas(int id)
        {
            help = new DBHelper();
            List<Trivias> lista = new List<Trivias>();
            string consulta = "SeleccionarPreguntas";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PID",id);
            help.miCommand.Parameters.Add(parametro1);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
           
            while (lector.Read())
            {
                Trivias triv = new Trivias();
                triv.idpregunta = Convert.ToInt32(lector["idPregunta"]);
                triv.pregunta = lector["pregunta"].ToString();
                triv.respuesta1= lector["respuesta1"].ToString();
                triv.respuesta2 = lector["respuesta2"].ToString();
                triv.respuesta3 = lector["respuesta3"].ToString();
                triv.correcto   =  Convert.ToInt32(lector["correcto"]);
                lista.Add(triv);
            }
            help.conn.Close();
            return lista;
        }
        public Trivias SeleccionarPregunta(int id)
        {
            help = new DBHelper(); 
            string consulta = "SeleccionarPregunta";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PPregunta", id);
            help.miCommand.Parameters.Add(parametro1);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            Trivias triv = new Trivias();
            while (lector.Read())
            {
                triv.idpregunta = Convert.ToInt32(lector["idPregunta"]);
                triv.pregunta = lector["pregunta"].ToString();
                triv.respuesta1 = lector["respuesta1"].ToString();
                triv.respuesta2 = lector["respuesta2"].ToString();
                triv.respuesta3 = lector["respuesta3"].ToString();
                triv.correcto = Convert.ToInt32(lector["correcto"]);
            }
            help.conn.Close();
            return triv;
        }

        public void ModificarPregunta(Trivias trivia)
        {
            help = new DBHelper();
            help.AbrirConParametros("ModificarPregunta");
            MySqlParameter parametro1 = new MySqlParameter("PID", trivia.id);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PPregunta", trivia.pregunta);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PRespuesta1", trivia.respuesta1);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PRespuesta2", trivia.respuesta2);
            help.miCommand.Parameters.Add(parametro4);

            MySqlParameter parametro5 = new MySqlParameter("PRespuesta3", trivia.respuesta3);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro6 = new MySqlParameter("PCorrecto", trivia.correcto);
            help.miCommand.Parameters.Add(parametro6);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }
        public void EliminarPregunta(int id)
        {
            help = new DBHelper();
            help.AbrirConParametros("EliminarPregunta");
            MySqlParameter parametro2 = new MySqlParameter("PPregunta",id);
            help.miCommand.Parameters.Add(parametro2);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }

        public void EliminarTrivia(int id)
        {
            help = new DBHelper();
            help.AbrirConParametros("EliminarTrivia");
            MySqlParameter parametro2 = new MySqlParameter("PTrivia", id);
            help.miCommand.Parameters.Add(parametro2);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public Trivias SeleccionarTriv(int id)
        {
            help = new DBHelper();
            List<Trivias> lista = new List<Trivias>();
            string consulta = "SeleccionarTrivia";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro2 = new MySqlParameter("PTrivia", id);
            help.miCommand.Parameters.Add(parametro2);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            Trivias triv = new Trivias();

            while (lector.Read())
            {
                triv.id = Convert.ToInt32(lector["idTrivia"]);
                triv.nombre = lector["nombre"].ToString();
                triv.imagen= lector["imagen"].ToString();

            }
            help.conn.Close();
            return triv;
        }
        public void AgregarPuntuacion(int puntuacion, int trivia, int user)
        {
            help = new DBHelper();
            help.AbrirConParametros("AgregarPuntaje");
            MySqlParameter parametro1 = new MySqlParameter("PPuntuacion", puntuacion);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PTrivia", trivia);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PUser",user);
            help.miCommand.Parameters.Add(parametro3);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();


        }
        public List<Trivias> Ranking(int id)
        {
            help = new DBHelper();
            List<Trivias> lista = new List<Trivias>();
            string consulta = "SeleccionarRanking";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro2 = new MySqlParameter("PTrivia", id);
            help.miCommand.Parameters.Add(parametro2);
            MySqlDataReader lector = help.miCommand.ExecuteReader();


            while (lector.Read())
            {            Trivias triv = new Trivias();
                triv.id = Convert.ToInt32(lector["idTrivia"]);
                triv.user = lector["nombre"].ToString();
                triv.puntuacion= Convert.ToInt32(lector["puntuacion"]);
                lista.Add(triv);
            }
            help.conn.Close();
            return lista;
        }
        public List<Trivias> MisTrivias(int id)
        {
            help = new DBHelper();
            List<Trivias> lista = new List<Trivias>();
            string consulta = "SelecccionarTriviasUsuario";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro2 = new MySqlParameter("PUser", id);
            help.miCommand.Parameters.Add(parametro2);
            MySqlDataReader lector = help.miCommand.ExecuteReader();


            while (lector.Read())
            {
                Trivias triv = new Trivias();
                triv.id = Convert.ToInt32(lector["idTrivia"]);
                triv.nombre = lector["trivnom"].ToString();
                triv.puntuacion = Convert.ToInt32(lector["puntuacion"]);
                lista.Add(triv);
            }
            help.conn.Close();
            return lista;
        }
        public bool SiYaJugo(int trivia, int user)
        {
            help = new DBHelper();
            List<Trivias> lista = new List<Trivias>();
            string consulta = "SeleccionarUsuariosTrivia";
            help.AbrirConParametros(consulta);

            MySqlParameter parametro2 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PTrivia", trivia);
            help.miCommand.Parameters.Add(parametro3);
            MySqlDataReader lector = help.miCommand.ExecuteReader();


            if (lector.Read())
            {
                help.conn.Close();
                return true;
            }
            else { return false; }
           
        }
    }
}