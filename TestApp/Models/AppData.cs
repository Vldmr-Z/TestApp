using Npgsql;
using System;
using System.Configuration;
using System.Data;
using System.Web;

namespace TestApp.Models
{
    public class AppData
    {
        string conn_string = ConfigurationManager.ConnectionStrings["ConnectionServer01"].ToString();

        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(conn_string);
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand
                {
                    Connection = conn,
                    CommandText = query,
                    CommandType = CommandType.Text
                };
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                adapter.Fill(dt);
                conn.Dispose();
            }
            catch { }
            return dt;
        }

        public object GetScalar(string query)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(conn_string);
                NpgsqlCommand cmd = new NpgsqlCommand
                {
                    Connection = conn,
                    CommandText = query,
                    CommandType = CommandType.Text
                };
                conn.Open();
                var result = cmd.ExecuteScalar();
                conn.Dispose();
                return result;
            }
            catch
            {
                return null;
            }
        }
    }
}