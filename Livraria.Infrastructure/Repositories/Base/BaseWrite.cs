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

        protected IDbTransaction BeginTransaction()
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            return Connection.BeginTransaction();
        }

        public virtual async Task<bool> Delete(T entity) => await Connection.DeleteAsync(entity);

        public virtual async Task<T?> GetById(int id) => await Connection.GetAsync<T>(id);

        public virtual async Task<IEnumerable<T>> GetAll() => await Connection.GetAllAsync<T>();

        public virtual async Task<bool> Insert(T entity) => await Connection.InsertAsync(entity) > 0;

        public virtual async Task<bool> Update(T entity) => await Connection.UpdateAsync(entity);
    }
}
