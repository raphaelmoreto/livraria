using Dapper.Contrib.Extensions;
using Livraria.Infrastructure.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositories.Base
{
    public class BaseRead<T> : IBaseRead<T> where T : class
    {
        protected readonly IDatabaseConnection DbConnection;

        protected readonly IDbConnection Connection;

        public BaseRead(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            Connection = DbConnection.GetConnection();
        }

        public virtual async Task<T> Get(int id) => await Connection.GetAsync<T>(id);

        public virtual async Task<IEnumerable<T>> GetAll() => await Connection.GetAllAsync<T>();
    }
}
