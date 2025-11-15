using Livraria.Infrastructure.Interfaces;
using System.Data;
using System.Text;

namespace Livraria.Infrastructure.Repositories._base
{
    public abstract class BaseRepository
    {
        private readonly IDatabaseConnection DbConnection;

        protected readonly IDbConnection Connection;

        protected StringBuilder SB;

        public BaseRepository(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            Connection = DbConnection.GetConnection();
            SB = new StringBuilder();
        }
    }
}
