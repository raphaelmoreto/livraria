using Dapper.Contrib.Extensions;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories._base;

namespace Livraria.Infrastructure.Repositories.Base
{
    public abstract class BaseWrite<T> : BaseRepository, IRepositoryWrite<T> where T : class
    {
        public BaseWrite(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public virtual async Task<bool> Delete(T entity) => await Connection.DeleteAsync(entity);

        public virtual async Task<T> Get(int id) => await Connection.GetAsync<T>(id);

        public virtual async Task<IEnumerable<T>> GetAll() => await Connection.GetAllAsync<T>();

        public virtual async Task<bool> Insert(T entity) => await Connection.InsertAsync(entity) > 0;

        public virtual async Task<bool> Update(T entity) => await Connection.UpdateAsync(entity);
    }
}
