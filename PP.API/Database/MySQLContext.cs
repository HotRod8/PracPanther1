using PP_Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Hosting.Server;

namespace PP.API.Database
{
    //To prevent SQL Injection, use 
    public class MySQLContext
    {
        private MySQLContext()
        {
            //Look up MySQL
            //Ex: Server = myServerAddress; Database = myDataBase; Uid = myUsername; Pwd = myPassword;
            connectionString = "Server=DESKTOP-VE9JE28;Database=PP_Database;Uid=pracpanther;Pwd=NewWebsite;";
        }

        private string connectionString;
        private static MySQLContext? instance;
        public static MySQLContext Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new MySQLContext();
                }
                return instance;
            }
        }

        //using lets the OS optimize the memory here
        //better for OS to do it instead of letting us accidentally break the computer  
        public List<Client> Get()
        {
            var results = new List<Client>();
            using (var conn = new SqlConnection(connectionString))
            {
                var sql = "select id, name from Clients";
                using (var cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        results.Add(new Client
                        {
                            Id = (int)reader[0],
                            Name = reader[1]?.ToString() ?? string.Empty
                        });
                    }
                }
            }

            return results;
        }
        public bool Insert(Client client) 
        {
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    //Adjust $"InsertClient" for MySQL
                    var sql = $"InsertClient";
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        conn.Open();
                        var Id = cmd.ExecuteScalar();
                    }
                }
            } catch(Exception) { return false; }

            return true;
        }
    }
}
