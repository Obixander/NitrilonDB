using Microsoft.Data.SqlClient;
using Nitrilon.Entities;
using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Nitrilon.DataAccess
{
    public class Repository
    {
        //this is used to create an connection to the database
        protected string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NitrilonDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private SqlConnection connection;

        public Repository()
        {
            if (!CanConnect())
            {
                throw new Exception("Cannot connect to the database");
            }
        }

        protected void CloseConnection()
        {
            if (connection != null && connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        protected SqlDataReader Execute(string sql)
        {
            if (sql is null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            connection = new(connectionString);
            SqlCommand command = new(sql, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            return reader;
        }

        public bool CanConnect()
        {
            try
            {
                SqlConnection sqlConnection = new(connectionString);
                sqlConnection.Open();
                sqlConnection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }




    }
}
