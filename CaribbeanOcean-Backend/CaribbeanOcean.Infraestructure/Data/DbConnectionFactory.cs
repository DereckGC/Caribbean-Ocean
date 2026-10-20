using Microsoft.Data.SqlClient;
using System.Data;

namespace CaribbeanOcean.Infraestructure.Data
{
    public class DbConnectionFactory(string connectionString)
    {
        public IDbConnection CreateConnection() => new SqlConnection(connectionString);
    }
}
