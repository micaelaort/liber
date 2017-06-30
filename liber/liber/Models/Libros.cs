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
        public string titulo { get; set; }
        public string autor { get; set; }
        public string sinopsis { get; set; }
        public string editorial { get; set; }
        public string reseña { get; set; }
        public int puntacion { get; set; }
        public string genero { get; set; }
        public string imagen { get; set; }
        public int id { get; set; }
        public string ingresado { get; set; }
        int cont;
        string consulta;



        DBHelper help = new DBHelper();
        Dictionary<int, Libros> dicLibros = new Dictionary<int, Libros>();
        List<Libros> listLibro = new List<Libros>();
        public List<Libros> SeleccionarLibrosUsuario(int iduser, string consulta)
        {

            help.AbrirConParametros(consulta);
            /*Le mando el nombreuser y me devuelve 5 o menos libros del usario*/
            MySqlParameter parametro1 = new MySqlParameter("PID", iduser);
            help.miCommand.Parameters.Add(parametro1);

            /*Me devuelve los libros del usuario*/

            MySqlDataReader lector = help.miCommand.ExecuteReader();

            while ((lector.Read()) && (cont < 6))
            {
                cont++;
                Libros librorecibido = new Libros();
                librorecibido.id = Convert.ToInt32(lector["idLibro"]);
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);


            }

            //help.miCommand.ExecuteNonQuery();
            //help.tran.Commit();
            help.conn.Close();
            return listLibro;
        }

        public bool Encontrar(string consulta, Libros libro)
        {
            consulta = "Buscar";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro1 = new MySqlParameter("PIngresado", libro.ingresado);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            if (lector.Read())
            {
                //help.miCommand.ExecuteNonQuery();
                //help.tran.Commit();
                help.conn.Close();
                return true;
            }
            else
            {

                help.conn.Close();
                return false;
            }
        }

        public List<Libros> SeleccionarGenero1(string consulta)
        {
            help.Abrir(consulta);
            listLibro = new List<Libros>();
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }
           ;
            help.conn.Close();
            return listLibro;
        }
        public List<Libros> SeleccionarGenero2(string consulta)
        {
            listLibro = new List<Libros>();
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;
        }

        public List<Libros> SeleccionarMejor(string consulta)
        {
            listLibro = new List<Libros>();
            help.Abrir(consulta);

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }
            help.conn.Close();

            return listLibro;
        }


        public List<Libros> TraerSeleccionado(string ingresado)
        {
            listLibro = new List<Libros>();
            consulta = "BuscarTitulo";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro1 = new MySqlParameter("PTitulo", ingresado);
            help.miCommand.Parameters.Add(parametro1);


            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            while (lector.Read())
            {
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }
            
            help.conn.Close();
            /***********************AUTORES****************************************/

            consulta = "BuscarAutor";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro2 = new MySqlParameter("PAutor", ingresado);
            help.miCommand.Parameters.Add(parametro2);


            lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            while (lector.Read())
            {
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }

            help.conn.Close();


            return listLibro;
        }



        public Libros BuscarLibro(string consulta, string titulolibro)
        {
            
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe libros*/
            MySqlParameter parametro1 = new MySqlParameter("PTitulo", titulolibro);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
          Libros librorecibido = new Libros();
            while (lector.Read())
            {
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }
            
            help.conn.Close();
            return librorecibido;


        }

        public List<Libros> Traer3libros()
        {
            consulta = "SeleccionarLibros";
            help.AbrirConParametros(consulta);
                
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            cont = 0;
            while (lector.Read()&& cont==0)
            {
                cont++;
                Libros librorecibido = new Libros();
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;
            
        }
        public List<Libros> Traer3Autores()
        {
            consulta = "SeleccionarLibros";
            help.AbrirConParametros(consulta);

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            cont = 0;
            while (lector.Read() && cont == 0)
            {
                cont++;
                Libros librorecibido = new Libros();
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.puntacion = Convert.ToInt32(lector["puntuacion"]);
                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;

        }

    }
}


       
