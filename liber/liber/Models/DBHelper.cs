using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using MySql.Data;

namespace liber.Models
{
    public class DBHelper
    {
        public MySqlCommand Comando;
        public MySqlConnection miConn;
        //public void Conectar(string consulta)
        //{
        //    MySqlConnection Conn = new MySqlConnection("server=localhost;Uid=root; Password=mica;Database=liber;Port=3306");
        //    Conn.Open();
        //    MySqlCommand miCommand = new MySqlCommand(consulta, Conn);
        //    miCommand.CommandType = CommandType.StoredProcedure;
        //    miCommand.CommandText = consulta;
        //    MySqlDataReader lector= miCommand.ExecuteReader();
        //    miCommand.ExecuteNonQuery();
        //    Conn.Close();
        //}
        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            }
        }

        public void Abrir(string consulta)
        {


            using (MySqlConnection miConn = new MySqlConnection(ConnectionString))
            {   
                miConn.Open();
                MySqlCommand miCommand = new MySqlCommand(consulta, miConn);
                miCommand.CommandType = CommandType.StoredProcedure;
                miCommand.CommandText = consulta;
                miCommand.ExecuteNonQuery();
                Comando = miConn.CreateCommand();

                //Nos aseguramos de cerrar la conexion
            }
        }
        public static DataTable EjecutarSelect(string select)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection miConn = new MySqlConnection(ConnectionString))
            {
                miConn.Open();
                MySqlCommand miCommand = new MySqlCommand(select, miConn);
                MySqlDataAdapter da = new MySqlDataAdapter(miCommand);
                da.Fill(dt);
                miConn.Close(); //Nos aseguramos de cerrar la conexion
            }
            return dt;
        }
    }
}