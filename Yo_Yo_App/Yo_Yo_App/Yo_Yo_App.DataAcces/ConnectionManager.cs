using System.Data.SqlClient;

namespace Yo_Yo_App.DataAccess
{
    public static class ConnectionManager
    {
        public static SqlConnection GetSqlConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
    }
