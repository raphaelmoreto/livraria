using Livraria.Infrasctructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Livraria.Infrasctructure.Connection
{
    public class DatabaseConnection : IDatabaseConnection
    {
        private readonly IConfiguration Configuration;

        private readonly string ConnectionString;

        public DatabaseConnection(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("SqlConnection")!;
        }

        public IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
