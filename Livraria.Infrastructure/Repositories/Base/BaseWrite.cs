using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories._base;

namespace Livraria.Infrastructure.Repositories.Base
{
    public abstract class BaseWrite<T> : BaseRepository, IBaseWrite<T> where T : class
    {
        public BaseWrite(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public abstract Task<bool> Delete(T entity);

        public abstract Task<T> Get(int id);

        public abstract Task<IEnumerable<T>> GetAll();

        public abstract Task<bool> Insert(T entity);

        public abstract Task<bool> Update(T entity);
    }
}
