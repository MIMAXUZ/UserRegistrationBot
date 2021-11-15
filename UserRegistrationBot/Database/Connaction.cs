using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace UDatabase
{
    class Connaction
    {
        public static string ConnectionString { get; private set; } = "server=localhost;user=root;database=userbotdb;port=3306;password=123456789";

        public static Dictionary<int, string> GetRegionList()
        {
            var data = new Dictionary<int, string> { };

            //your MySQL connection string
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                //SQL Query to execute
                //selecting only first 10 rows for demo
                string sql = "select id, name_uz as name from regions";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //object result = cmd.ExecuteScalar();
                //read the data
                while (rdr.Read())
                {
                    data.Add(Convert.ToInt32(rdr[0]), Convert.ToString(rdr[1]));
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                rdr.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            conn.Close();
            Console.WriteLine("Connection Closed. Press any key to exit...");
            return data;
        }

        public static Dictionary<int, string> GetOrganizations()
        {
            var data = new Dictionary<int, string> { };

            //your MySQL connection string
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
                string sql = "select id, name_uz as name from organizations";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();
                //object result = cmd.ExecuteScalar();
                //read the data
                while (rdr.Read())
                {
                    data.Add(Convert.ToInt32(rdr[0]), Convert.ToString(rdr[1]));
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                }
                rdr.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            conn.Close();
            //Console.WriteLine("Connection Closed. Press any key to exit...");
            return data;
        }

        public static bool DBConn()
        {
            //your MySQL connection string
            string connStr = "server=localhost;user=root;database=userbotdb;port=3306;password=123456789";
            bool result = false;
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                result = true;
                //SQL Query to execute
                //selecting only first 10 rows for demo
                //string sql = "select * from sakila.actor limit 0,10;";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);
                //MySqlDataReader rdr = cmd.ExecuteReader();

                ////read the data
                //while (rdr.Read())
                //{
                //    Console.WriteLine(rdr[0] + " -- " + rdr[1] + " -- " + rdr[2]);
                //}
                //rdr.Close();
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            conn.Close();
            Console.WriteLine("Connection Closed. Press any key to exit...");
            return result;
        }

        //Starting write to database
        public static bool InserApplication (string FirstName, string LastName, string MiddleName, string Photo, int Location, int Organization )
        {
            bool result = false;
            MySqlConnection conn = new MySqlConnection(ConnectionString);
            try
            {
                conn.Open();
                //SQL Query to execute
                //insert Query
                // we are inserting actor_id, first_name, last_name, last_updated columns data

                string rtn = "InsertApplications";
                MySqlCommand cmd = new MySqlCommand(rtn, conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@MiddleName", MiddleName);
                cmd.Parameters.AddWithValue("@photo", Photo);
                cmd.Parameters.AddWithValue("@Location", Location);
                cmd.Parameters.AddWithValue("@org", Organization);

                //string sql = "INSERT INTO applications (first_name, last_name, middle_name, region_id, organization_id, photo) VALUES (" + FirstName + ", " + LastName + ", " + MiddleName + ", " +  Location + ", " +  Organization + ", " + Photo + ")";
                //string sql = "INSERT INTO sakila.actor VALUES ('202','First Name Actor test','Last Name Actor test', '2020-11-05 04:34:33')";
                //MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                result = true;
            }
            catch (Exception err)
            {
                Console.WriteLine(err.ToString());
            }
            conn.Close();
            return result;
        }
    }
}
