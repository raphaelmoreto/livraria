using Livraria.Infrastructure.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositories._base
{
    public abstract class BaseRepository
    {
        private readonly IDatabaseConnection DbConnection;

        protected readonly IDbConnection Connection;

        public BaseRepository(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            Connection = DbConnection.GetConnection();
        }
    }
}
