using System.Data.SqlClient;

namespace GesRelationClient.Models
{
    public class DbConnectionManager
    {
        private readonly string _connectionString;

        public DbConnectionManager(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
