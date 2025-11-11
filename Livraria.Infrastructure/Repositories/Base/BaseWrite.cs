using Dapper.Contrib.Extensions;
using Livraria.Infrastructure.Interfaces;
using System.Data;

namespace Livraria.Infrastructure.Repositories.Base
{
    public class BaseWrite<T> : IBaseWrite<T> where T : class
    {
        protected readonly IDatabaseConnection DbConnection;

        protected readonly IDbConnection Connection;

        public BaseWrite(IDatabaseConnection dbConnection)
        {
            DbConnection = dbConnection;
            Connection = DbConnection.GetConnection();
        }

        public virtual async Task<bool> Delete(T entity) => await Connection.DeleteAsync(entity);

        public virtual async Task<bool> Insert(T entity) => await Connection.InsertAsync(entity) > 0;

        public virtual async Task<bool> Update(T entity) => await Connection.UpdateAsync(entity);
    }
}
