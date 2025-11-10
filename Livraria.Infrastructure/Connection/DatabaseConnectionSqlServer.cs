using Livraria.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Livraria.Infrastructure.Connection
{
    public class DatabaseConnectionSqlServer : IDatabaseConnection
    {
        private readonly IConfiguration configuration;

        private readonly string connectionString;

        public DatabaseConnectionSqlServer(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("SqlConnection")!;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
