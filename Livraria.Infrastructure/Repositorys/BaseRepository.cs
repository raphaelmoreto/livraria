using Dapper.Contrib.Extensions;
using Livraria.Infrastructure.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositorys
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly IDatabaseConnection DbConnection;

        private readonly IDbConnection Connection;

        public BaseRepository(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            Connection = DbConnection.GetConnection();
        }

        public virtual async Task<bool> Delete(T entity) => await Connection.DeleteAsync(entity);

        public virtual async Task<T> Get(int id) => await Connection.GetAsync<T>(id);

        public virtual async Task<IEnumerable<T>> GetAll() => await Connection.GetAllAsync<T>();

        public virtual async Task<bool> Insert(T entity) => await Connection.InsertAsync(entity) > 0;

        public virtual async Task<bool> Update(T entity) => await Connection.UpdateAsync(entity);
    }
}
