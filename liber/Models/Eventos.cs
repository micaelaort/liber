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
    public class Eventos
    {
        public int idevento { get; set; }
        public int idusuario { get; set; }
        public string descripcion { get; set; }
        public string titulo { get; set; }
        public DateTime fecha { get; set; }
        public string imagen { get; set; }
        public string asistencia { get; set; }

        public DateTime horariocomienzo { get; set; }
        public DateTime horariofinal { get; set; }
        public DateTime fechafinal { get; set; }
        string consulta;
        DBHelper help = new DBHelper();
        Dictionary<int, Eventos> dicEventos = new Dictionary<int, Eventos>();
        List<Eventos> ListEventos = new List<Eventos>();


        public List<Eventos> MostrarEvento(string consulta)
        {
            ListEventos = new List<Eventos>();
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Eventos evento = new Eventos();
                evento.idevento = Convert.ToInt32(lector["idEvento"]);
                evento.titulo = lector["Titulo"].ToString();
                evento.fecha = Convert.ToDateTime(lector["Fecha"]);
                evento.descripcion = lector["Descripcion"].ToString();
                evento.imagen = lector["Imagen"].ToString();
                evento.fechafinal = Convert.ToDateTime(lector["Fechafinal"]);
                evento.horariocomienzo = Convert.ToDateTime(lector["Horariocomienzo"]);
                evento.horariofinal = Convert.ToDateTime(lector["Horariofinal"]);


                ListEventos.Add(evento);
            }
            help.conn.Close();
            return ListEventos;
        }

        public void EliminarEvento(string consulta, int idevento)
        {

            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PId", idevento);
            help.miCommand.Parameters.Add(parametro1);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public bool AgregarEvento(string consulta, Eventos evento)
        {
            if (evento.fecha.ToShortDateString() != "1/1/0001")
            {
                if (evento.fechafinal.ToShortDateString() != "1/1/0001" && evento.fechafinal.Date >= DateTime.Now.Date)
                {
                    help.AbrirConParametros(consulta);

                    MySqlParameter parametro1 = new MySqlParameter("PTitulo", evento.titulo);
                    help.miCommand.Parameters.Add(parametro1);

                    MySqlParameter parametro2 = new MySqlParameter("PImagen", evento.imagen);
                    help.miCommand.Parameters.Add(parametro2);

                    MySqlParameter parametro3 = new MySqlParameter("PFecha", evento.fecha);
                    help.miCommand.Parameters.Add(parametro3);

                    MySqlParameter parametro4 = new MySqlParameter("PDescripcion", evento.descripcion);
                    help.miCommand.Parameters.Add(parametro4);
                    MySqlParameter parametro6 = new MySqlParameter("PFechafinal", evento.fechafinal);
                    help.miCommand.Parameters.Add(parametro6);

                    MySqlParameter parametro7 = new MySqlParameter("PHorariocomienzo", evento.horariocomienzo);
                    help.miCommand.Parameters.Add(parametro7);

                    MySqlParameter parametro8 = new MySqlParameter("PHorariofinal", evento.horariofinal);
                    help.miCommand.Parameters.Add(parametro8);
                    
                    help.miCommand.ExecuteNonQuery();
                    help.tran.Commit();
                    help.conn.Close();
                    return true;
                }
                else
                {
                    if (evento.fecha.Date >= DateTime.Now.Date)
                    {
                        help.AbrirConParametros(consulta);

                        MySqlParameter parametro1 = new MySqlParameter("PTitulo", evento.titulo);
                        help.miCommand.Parameters.Add(parametro1);

                        MySqlParameter parametro2 = new MySqlParameter("PImagen", evento.imagen);
                        help.miCommand.Parameters.Add(parametro2);

                        MySqlParameter parametro3 = new MySqlParameter("PFecha", evento.fecha);
                        help.miCommand.Parameters.Add(parametro3);

                        MySqlParameter parametro4 = new MySqlParameter("PDescripcion", evento.descripcion);
                        help.miCommand.Parameters.Add(parametro4);
                        MySqlParameter parametro6 = new MySqlParameter("PFechafinal", evento.fechafinal);
                        help.miCommand.Parameters.Add(parametro6);

                        MySqlParameter parametro7 = new MySqlParameter("PHorariocomienzo", evento.horariocomienzo);
                        help.miCommand.Parameters.Add(parametro7);

                        MySqlParameter parametro8 = new MySqlParameter("PHorariofinal", evento.horariofinal);
                        help.miCommand.Parameters.Add(parametro8);

                        help.miCommand.ExecuteNonQuery();
                        help.tran.Commit();
                        help.conn.Close();
                        return true;
                    }
                    else
                    { return false; }
                  
                }
            }
            else
            {
                
                return false;
            }

            
        }

        //deberia comparar que fechainicial no sea mayor a fechafinal



        public Eventos SeleccionarEvento(int id)
        {
            consulta = "SeleccionarEvento";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro4 = new MySqlParameter("PID", id);
            help.miCommand.Parameters.Add(parametro4);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            Eventos evento = new Eventos();

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {

                evento.idevento = Convert.ToInt32(lector["idEvento"]);
                evento.titulo = lector["Titulo"].ToString();
                evento.descripcion = lector["descripcion"].ToString();
                evento.fecha = Convert.ToDateTime(lector["fecha"]);
                evento.imagen = lector["imagen"].ToString();
                evento.fechafinal = Convert.ToDateTime(lector["Fechafinal"]);
                evento.horariocomienzo = Convert.ToDateTime(lector["Horariocomienzo"]);
                evento.horariofinal = Convert.ToDateTime(lector["Horariofinal"]);

            }
            help.conn.Close();
            return evento;
        }


        public void ModificarEvento(string consulta, Eventos evento)
        {
            help.AbrirConParametros(consulta);
            MySqlParameter parametro5 = new MySqlParameter("PidEvento", evento.idevento);
            help.miCommand.Parameters.Add(parametro5);

            MySqlParameter parametro1 = new MySqlParameter("PTitulo", evento.titulo);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PImagen", evento.imagen);
            help.miCommand.Parameters.Add(parametro2);

            MySqlParameter parametro3 = new MySqlParameter("PFecha", evento.fecha);
            help.miCommand.Parameters.Add(parametro3);

            MySqlParameter parametro4 = new MySqlParameter("PDescripcion", evento.descripcion);
            help.miCommand.Parameters.Add(parametro4);

            MySqlParameter parametro6 = new MySqlParameter("PFechafinal", evento.fechafinal);
            help.miCommand.Parameters.Add(parametro6);

            MySqlParameter parametro7 = new MySqlParameter("PHorariocomienzo", evento.horariocomienzo);
            help.miCommand.Parameters.Add(parametro7);

            MySqlParameter parametro8 = new MySqlParameter("PHorariofinal", evento.horariofinal);
            help.miCommand.Parameters.Add(parametro8);


            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public List<Eventos> TraerEventos()
        {
            string consulta = "SeleccionarEventosOrdenados";
            ListEventos = new List<Eventos>();
            help.Abrir(consulta);
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Eventos evento = new Eventos();
               evento.idevento = Convert.ToInt32(lector["idEvento"]);evento.titulo = lector["Titulo"].ToString();
                    evento.fecha = Convert.ToDateTime(lector["Fecha"]);
                    evento.descripcion = lector["Descripcion"].ToString();
                    evento.imagen = lector["Imagen"].ToString();
                ListEventos.Add(evento);
            

}
            help.conn.Close();
            return ListEventos;
        }

        public bool EstaAgendado(int idevento, int iduser)
        {
            consulta = "SeleccionarEventoUsuario";
            help.AbrirConParametros(consulta);
            /*Le mando lo ingresado y debe buscar en autores, libros o genero*/
            MySqlParameter parametro1 = new MySqlParameter("PEvento", idevento);
            help.miCommand.Parameters.Add(parametro1);
            MySqlParameter parametro2 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro2);

            ListEventos = new List<Eventos>();
        
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

        public void ActualizarAsistencia(int idevento, int idusuario, string status)
        {
            consulta = "ActualizarAsistencia";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", idusuario);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PEvento", idevento);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PStatus", status);
            help.miCommand.Parameters.Add(parametro3);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }

        public void AñadirAsistencia(int idevento, int idusuario,  string status)
        {
            consulta = "AgregarAsistencia";
            help.AbrirConParametros(consulta);
            MySqlParameter parametro1 = new MySqlParameter("PUser", idusuario);
            help.miCommand.Parameters.Add(parametro1);

            MySqlParameter parametro2 = new MySqlParameter("PEvento", idevento);
            help.miCommand.Parameters.Add(parametro2);
            MySqlParameter parametro3 = new MySqlParameter("PStatus", status);
            help.miCommand.Parameters.Add(parametro3);

            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            help.conn.Close();
        }


        public Eventos EventoUsuario(int idevento, int iduser)
        {
            help.AbrirConParametros(consulta);
            MySqlParameter parametro4 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro4);
            MySqlParameter parametro5 = new MySqlParameter("PEvento", idevento);
            help.miCommand.Parameters.Add(parametro5);
            help.miCommand.ExecuteNonQuery();
            help.tran.Commit();
            Eventos evento = new Eventos();

            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {

                evento.idevento    = Convert.ToInt32(lector["idEvento"]);
                evento.titulo      = lector["Titulo"].ToString();
                evento.descripcion = lector["descripcion"].ToString();
                evento.fecha       = Convert.ToDateTime(lector["fecha"]);
                evento.imagen      = lector["imagen"].ToString();
                evento.asistencia  = lector["Asistencia"].ToString();
                evento.fechafinal = Convert.ToDateTime(lector["Fechafinal"]);
                evento.horariocomienzo = Convert.ToDateTime(lector["Horariocomienzo"]);
                evento.horariofinal = Convert.ToDateTime(lector["Horariofinal"]);

            }
            help.conn.Close();
            return evento;


        }


        public List<Eventos> ListadoMisEventos(int iduser)
        {
            consulta = "SeleccionarMisEventos";
            help.Abrir(consulta);
            help.AbrirConParametros(consulta);
            MySqlParameter parametro4 = new MySqlParameter("PUser", iduser);
            help.miCommand.Parameters.Add(parametro4);
            ListEventos = new List<Eventos>();
           
            MySqlDataReader lector = help.miCommand.ExecuteReader();
            while (lector.Read())
            {
                Eventos evento = new Eventos();
                evento.idevento = Convert.ToInt32(lector["idEvento"]);
                evento.titulo = lector["Titulo"].ToString();
                evento.fecha = Convert.ToDateTime(lector["Fecha"]);
                evento.descripcion = lector["Descripcion"].ToString();
                evento.imagen = lector["Imagen"].ToString();
                evento.fechafinal = Convert.ToDateTime(lector["Fechafinal"]);
                evento.horariocomienzo = Convert.ToDateTime(lector["Horariocomienzo"]);
                evento.horariofinal = Convert.ToDateTime(lector["Horariofinal"]);


                ListEventos.Add(evento);
            }
            help.conn.Close();
            return ListEventos;
        
    }



    }
    }