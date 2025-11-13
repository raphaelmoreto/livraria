using Dapper.Contrib.Extensions;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories._base;

namespace Livraria.Infrastructure.Repositories.Base
{
    public class BaseWrite<T> : BaseRepository<T>, IBaseWrite<T> where T : class
    {
        public BaseWrite(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public override async Task<bool> Delete(T entity) => await Connection.DeleteAsync(entity);

        public override async Task<bool> Insert(T entity) => await Connection.InsertAsync(entity) > 0;

        public override async Task<bool> Update(T entity) => await Connection.UpdateAsync(entity);
    }
}
