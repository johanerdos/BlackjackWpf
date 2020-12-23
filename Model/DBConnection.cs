using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackWpf.Model
{
    class DBConnection
    {
        public static SqlConnection conn;
        public static String connString = @"Data Source=DESKTOP-M552MVR\SQLExpress;Initial Catalog=BlackJack;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            conn = new SqlConnection(connString);
            conn.Open();
            if (conn.State == System.Data.ConnectionState.Open)
            {
                
                return conn;
            }
            return null;
        }
        public static void Disconnect()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    conn.Close();
                }

                catch (SqlException)
                {
                    
                }

            }

        }
    }
}
