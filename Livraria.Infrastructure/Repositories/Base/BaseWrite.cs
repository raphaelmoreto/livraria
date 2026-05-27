using Dapper.Contrib.Extensions;
using Livraria.Domain.Interfaces.Repositories.Base;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories._base;
using System.Data;

namespace Livraria.Infrastructure.Repositories.Base
{
    public abstract class BaseWrite<T> : BaseRepository, IDelete<T>, IGetAll<T>,  IGetById<T>, IInsert<T>, IUpdate<T> where T : class
    {
        public BaseWrite(IDatabaseConnection dbConnection) : base(dbConnection) { }

        protected IDbTransaction BeginTransaction(IDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed)
                connection.Open();

            return connection.BeginTransaction();
        }

        public virtual async Task<bool> Delete(T entity)
        {
            using var connection = CreateConnection();
            return await connection.DeleteAsync(entity);
        }

        public virtual async Task<T?> GetById(int id)
        {
            using var connection = CreateConnection();
            return await connection.GetAsync<T>(id);
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            using var connection = CreateConnection();
            return await connection.GetAllAsync<T>();
        }

        public virtual async Task<bool> Insert(T entity)
        {
            using var connection = CreateConnection();
            return await connection.InsertAsync(entity) > 0;
        }

        public virtual async Task<bool> Update(T entity)
        {
            using var connection = CreateConnection();
            return await connection.UpdateAsync(entity);
        }
    }
}
