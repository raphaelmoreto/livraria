using Livraria.Infrastructure.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositories._base
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IDatabaseConnection DbConnection;

        protected readonly IDbConnection Connection;

        public BaseRepository(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            Connection = DbConnection.GetConnection();
        }

        public abstract Task<bool> Delete(T entity);

        public abstract Task<T> Get(int id);

        public abstract Task<IEnumerable<T>> GetAll();

        public abstract Task<bool> Insert(T entity);

        public abstract Task<bool> Update(T entity);
    }
}
