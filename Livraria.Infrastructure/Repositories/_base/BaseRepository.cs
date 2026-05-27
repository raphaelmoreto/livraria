using Livraria.Infrastructure.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositories._base
{
    public abstract class BaseRepository
    {
        private readonly IDatabaseConnection DbConnection;

        //NÃO UTILIZAR CONEXÃO COMPARTILHADA ENTRE AS CLASSES FILHAS (BaseRead e BaseWrite)! FAZER COM QUE CADA OPERAÇÃO CRIE SUA CONEXÃO (using var connection = DbConnection.GetConnection();)
        //protected readonly IDbConnection Connection;

        public BaseRepository(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            //Connection = DbConnection.GetConnection();
        }

        protected IDbConnection CreateConnection()
        {
            return DbConnection.GetConnection();
        }
    }
}
