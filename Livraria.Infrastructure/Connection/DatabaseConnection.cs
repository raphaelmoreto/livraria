using Livraria.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Diagnostics;

namespace Livraria.Infrastructure.Connection
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
            if (Debugger.IsAttached)
            {
                //APENAS COLOCADO PARA SIMULAR SE CASO TIVESSE 2 BANCOS! UM PARA PRODUÇÃO E OUTRO DE TESTE
                return new SqlConnection(ConnectionString);
            }

            return new SqlConnection(ConnectionString);
        }
    }
}
