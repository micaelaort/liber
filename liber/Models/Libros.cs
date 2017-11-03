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
        public int promedio { get; set; }
        public string genero { get; set; }
        public string imagen { get; set; }
        public int id { get; set; }
        public string ingresado { get; set; }
        public string guardado { get; set; }
        public string leido { get; set; }
        public string visible{ get; set; }
        public int idgenero;
        int cont;
        string consulta;

        DBHelper help = new DBHelper();
        List<Libros> listLibro = new List<Libros>();
        public List<Libros> SeleccionarLibrosUsuario(int iduser)
        {
            consulta = "Libros5Usuarios";
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
            int cont = 0;
            help.Abrir(consulta);
            listLibro = new List<Libros>();
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read() && cont<=4)
            {
                cont++;
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();

                listLibro.Add(librorecibido);
            }
           ;
            help.conn.Close();
            return listLibro;
        }
        public List<Libros> SeleccionarGenero2(string consulta)
        {
            int cont=0;
            listLibro = new List<Libros>();
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read()&& cont<=4)
            {
                Libros librorecibido = new Libros();
                cont++;
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();

                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;
        }

        public List<Libros> SeleccionarMejor(string consulta)
        {
            listLibro = new List<Libros>();
            help.Abrir(consulta);
            int cont = 0;
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read()&& cont <=4)
            {
                Libros librorecibido = new Libros();
                cont++;
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();

                listLibro.Add(librorecibido);
            }
            help.conn.Close();

            return listLibro;
        }

        public List<Libros> Links(string consulta, string hola)
        {
            listLibro = new List<Libros>();
            consulta = "Buscar";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro1 = new MySqlParameter("PIngresado", hola);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Libros librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();

                listLibro.Add(librorecibido);
            }
            return listLibro;
        }
        public List<Libros> TraerSeleccionado(string ingresado)
        {
            listLibro = new List<Libros>();
            consulta = "Buscar";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro1 = new MySqlParameter("PIngresado", ingresado);
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
                librorecibido = new Libros();
                librorecibido.id = Convert.ToInt32(lector["idLibro"]);
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();
                librorecibido.promedio = Convert.ToInt32(lector["promedio"]);
                librorecibido.idgenero = Convert.ToInt32(lector["idGenero"]);
                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return librorecibido;


        }
        public int EncontrarGenero(int user)
        {

            string consulta = "SeleccionarGenerosRecomendados";

            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", user);
            help.miCommand.Parameters.Add(parametro1);
            Libros librorecibido = new Libros();
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
               librorecibido.idgenero = Convert.ToInt32(lector["idGenero"]);
              
            }

            help.conn.Close();
            return librorecibido.idgenero;
        }
        public List<Libros> BuscarGenero(int genero)
        {
          
            string consulta = "BuscarGenero";
            DBHelper help = new DBHelper();
            help.AbrirConParametros(consulta);
            MySqlParameter parametro2 = new MySqlParameter("PGenero",genero);
            help.miCommand.Parameters.Add(parametro2);
            Libros librorecibido = new Libros();
            MySqlDataReader lector = help.miCommand.ExecuteReader();
                while (lector.Read())
                {

                    librorecibido = new Libros();
                    //librorecibido.id = Convert.ToInt32(lector["idLibro"]);
                    librorecibido.titulo = lector["titulo"].ToString();
                    librorecibido.autor = lector["autor"].ToString();
                    librorecibido.imagen = lector["tapa"].ToString();
                    librorecibido.sinopsis = lector["sinopsis"].ToString();
                    librorecibido.genero = lector["genero"].ToString();

                librorecibido.idgenero = genero;
                    listLibro.Add(librorecibido); 
            }

            help.conn.Close();
        
            return listLibro;
        }

        public Libros Opciones(int idlibro, int iduser)
        {
            consulta = "SeleccionarOpciones";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro2);


            MySqlDataReader lector = help.miCommand.ExecuteReader();
            Libros librorecibido = new Libros();
            while (lector.Read())
            {
                
                librorecibido.leido    = lector["Leido"].ToString();
                librorecibido.guardado = lector["Guardado"].ToString();
            }

            help.conn.Close();
            return librorecibido;
        }

        public void ActualizarLeido(int iduser, int idlibro,string status)
        {
            consulta = "ActualizarLeido";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PStatus", status);
            help.miCommand.Parameters.Add(parametro3);

        
            help.tran.Commit();
            help.conn.Close();
        }

        public void ActualizarGuardado(int iduser, int idlibro, string status)
        {
            consulta = "ActualizarGuardado";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PStatus", status);
            help.miCommand.Parameters.Add(parametro3);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public List<Libros> LibrosPendientes(int idUser)
        {
            consulta = "SeleccionarLeidos";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe libros*/
            MySqlParameter parametro1 = new MySqlParameter("PUser", idUser);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            Libros librorecibido = new Libros();
            while (lector.Read())
            {
                librorecibido = new Libros();
             
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();
           
            
                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;


        }

        public List<Libros> LibrosBiblioteca(int idUser)
        {

            consulta = "SeleccionarBiblioteca";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe libros*/
            MySqlParameter parametro1 = new MySqlParameter("PUser", idUser);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            Libros librorecibido = new Libros();
            while (lector.Read())
            {
                librorecibido = new Libros();

                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autor"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["genero"].ToString();


                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;

        }

         public bool EstaAgendado(int idlibro, int iduser)
        {
            consulta = "SeleccionarLeidoUsuario";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro1 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro2);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
           if (lector.Read())
            {
                help.conn.Close();
                return true;
            }

            else
            {
                help.conn.Close();
                return false;
            }
            
        }

        public void AñadirLeido(int iduser, int idlibro, string status)
        {
            consulta = "AgregarLeido";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PStatus", status);
            help.miCommand.Parameters.Add(parametro3);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void AñadirGuardado(int iduser, int idlibro, string status)
        {
            consulta = "AgregarGuardado";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PLibro", idlibro);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PStatus", status);
            help.miCommand.Parameters.Add(parametro3);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }


        public List<Libros> SeleccionarLibros()
        {

            consulta = "SeleccionarLibros";
            help.AbrirConParametros(consulta);
         
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            Libros librorecibido = new Libros();
            while (lector.Read())
            {
                librorecibido = new Libros();
                librorecibido.id = Convert.ToInt32(lector["idLibro"]);
                librorecibido.titulo = lector["titulo"].ToString();
                librorecibido.autor = lector["autornom"].ToString();
                librorecibido.imagen = lector["tapa"].ToString();
                librorecibido.sinopsis = lector["sinopsis"].ToString();
                librorecibido.genero = lector["generonom"].ToString();
                librorecibido.visible = lector["visible"].ToString();

                listLibro.Add(librorecibido);
            }

            help.conn.Close();
            return listLibro;

        }
        public bool EstaOculto(int id)
        {

            consulta = "SeleccionarOculto";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe libros*/
            MySqlParameter parametro1 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro1);
            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            Libros librorecibido = new Libros();
            while (lector.Read())
            {
                librorecibido = new Libros();

                librorecibido.visible = lector["visible"].ToString();
                
                if (librorecibido.visible == "false")
                {
                    return false;
                     
                }
               
            }

            help.conn.Close();
            return true;

        }

        public void AgregarLibro(Libros lib)
        {
            consulta = "AgregarLibro";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PTitulo",lib.titulo);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PSinopsis", lib.sinopsis);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PTapa", lib.imagen);
            help.miCommand.Parameters.Add(parametro3);
            MySqlParameter parametro4 = new MySqlParameter("PAutor", lib.autor);
            help.miCommand.Parameters.Add(parametro4);
            MySqlParameter parametro7 = new MySqlParameter("PVislbe", visible);
            help.miCommand.Parameters.Add(parametro7);
            MySqlParameter parametro5 = new MySqlParameter("PGenero", lib.genero);
            help.miCommand.Parameters.Add(parametro5);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void ModificarTitulo(int id, string titulo)
        {
            consulta = "ActualizarTitulo";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro",id);
            help.miCommand.Parameters.Add(parametro6);
            MySqlParameter parametro1 = new MySqlParameter("PTitulo", titulo);
            help.miCommand.Parameters.Add(parametro1);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void ModificarGenero(int id, string genero)
        {
            consulta = "ActualizarGenero";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro", id);
            help.miCommand.Parameters.Add(parametro6);
            MySqlParameter parametro5 = new MySqlParameter("PGenero",Convert.ToInt32(genero));
            help.miCommand.Parameters.Add(parametro5);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }



        public void ModificarAutor(int id,string  autor)
        {
            consulta = "ActualizarAutor";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro", id);
            help.miCommand.Parameters.Add(parametro6);
            MySqlParameter parametro4 = new MySqlParameter("PAutor", Convert.ToInt32(autor));
            help.miCommand.Parameters.Add(parametro4);
            //help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void ModificarSinopsis(int id, string sinopsis)
        {
            consulta = "ActualizarSinopsis";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro", id);
            help.miCommand.Parameters.Add(parametro6);
            MySqlParameter parametro2 = new MySqlParameter("PSinopsis",sinopsis);
            help.miCommand.Parameters.Add(parametro2);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void ModificarImagen(int id, string imagen)
        {
            consulta = "ActualizarTapa";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro", id);
            help.miCommand.Parameters.Add(parametro6);
            MySqlParameter parametro3 = new MySqlParameter("PImagen", imagen);
            help.miCommand.Parameters.Add(parametro3);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void ModificarVisible(int id, string visible)
        {
            consulta = "ActualizarVisible";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro", id);
            help.miCommand.Parameters.Add(parametro6);
            MySqlParameter parametro7= new MySqlParameter("PVisible", visible);
            help.miCommand.Parameters.Add(parametro7);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }
        public Libros SeleccionarLibro(int id)
        {

            consulta = "SeleccionarLibro";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PLibro", id);
            help.miCommand.Parameters.Add(parametro6);

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
                librorecibido.genero = lector["genero"].ToString();
                librorecibido.visible = lector["visible"].ToString();

               
            }

            help.conn.Close();
            return librorecibido;

        }

        public int SeleccionarIDLibro(string titulo)
        {

            consulta = "SeleccionarIDLibro";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro6 = new MySqlParameter("PTitulo", titulo);
            help.miCommand.Parameters.Add(parametro6);

            /*Me devuelve los libros del usuario*/
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            //Se fija si buscarlibro devuelve algo, si vuelve vacio busca por autor
            Libros librorecibido = new Libros();
            while (lector.Read())
            {
              
                librorecibido.id = Convert.ToInt32(lector["idLibro"]);
             }

            help.conn.Close();
            return librorecibido.id;

        }
    }
}


       
