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
        public MySqlCommand miCommand;
        public MySqlDataReader Data;
        public MySqlConnection conn;
        public MySqlTransaction tran;

        public void Abrir()
        {
            conn = new MySqlConnection();
            string proveedor = "server=127.0.0.1;Database=liber;Uid=root;Password=mica2511;Port=3306";
            conn.ConnectionString = proveedor;
            conn.Open();
            miCommand = conn.CreateCommand();
            miCommand.CommandType = CommandType.StoredProcedure;
        }
        public void AbrirConParametros(string consulta)
        {
            conn = new MySqlConnection();
            string proveedor = "server=127.0.0.1;Database=liber;Uid=root;Password=mica2511;Port=3306";
            conn.ConnectionString = proveedor;
            conn.Open();
            miCommand = new MySqlCommand(consulta,conn,tran);
            tran = conn.BeginTransaction();
            miCommand = conn.CreateCommand();
            miCommand.CommandType = CommandType.StoredProcedure;
            miCommand.CommandText = consulta;
        }


      


    }
}