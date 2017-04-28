using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace liber.Models
{
    public class DBHelper
    {
        private string query;

        public static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            }
        }

        private void Abrir()
        {


            using (MySqlConnection miConn = new MySqlConnection(ConnectionString))
            {
                miConn.Open();
                MySqlCommand miCommand = new MySqlCommand(query,miConn);
                miCommand.CommandType = CommandType.StoredProcedure;
                miCommand.ExecuteNonQuery();
                miConn.Close(); //Nos aseguramos de cerrar la conexion
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