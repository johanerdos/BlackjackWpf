using BlackjackWpf.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackWpf.DAL
{
    class PlayerDAL
    {
        private static byte[] DESKey = { 200, 5, 78, 232, 9, 6, 0, 4 };
        private static byte[] DESInitializationVector = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public void AddPlayer(string name, int age, double wallet, string pwd)
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = DBConnection.GetConnection();
                comm.CommandText =
                    "INSERT INTO Persons " +
                    "VALUES ('" + name + "', '" + age + "', '" + wallet + "', '" + pwd + "')";
                comm.ExecuteNonQuery();

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public Player FindPlayer(string name)
        {
            Player p = new Player();
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = DBConnection.GetConnection();
                comm.CommandText = "SELECT * FROM Persons WHERE name =('" + name + "')";
                SqlDataReader myReader = comm.ExecuteReader();
                while (myReader.Read())
                {
                    p.Name = myReader.GetString(0);
                    p.Age = myReader.GetInt32(1);
                    p.Wallet = (double)myReader.GetDecimal(2);
                    p.Password = myReader.GetString(4);
                    return p;
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

            }
            return null;
        }

        
        public Player LogInPlayer(string name, string pwd)
        {

            Player temp = new Player();
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = DBConnection.GetConnection();
                comm.CommandText = "SELECT * FROM Persons WHERE name =('" + name + "')";
                SqlDataReader myReader = comm.ExecuteReader();
                
                while (myReader.Read())
                {
                    temp.Name = myReader.GetString(0);
                    temp.Age = myReader.GetInt32(1);
                    temp.Wallet = (double)myReader.GetDecimal(2);
                    temp.Password = myReader.GetString(4);
                    
                }
                if (temp.Password.Equals(pwd))
                {
                    return temp;
                }  

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);

            }
            return null;
        }
    }
}
