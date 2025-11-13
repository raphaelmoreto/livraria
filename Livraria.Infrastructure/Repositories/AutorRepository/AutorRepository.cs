using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.AutorRepositories
{
    public class AutorRepository : BaseWrite<AutorEntity>, IBaseWrite<AutorEntity>
    {
        public AutorRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public override async Task<bool> Delete(AutorEntity entity) => await Connection.DeleteAsync(entity);

        public override async Task<AutorEntity> Get(int id) => await Connection.GetAsync<AutorEntity>(id);

        public override async Task<IEnumerable<AutorEntity>> GetAll() => await Connection.GetAllAsync<AutorEntity>();

        public override async Task<bool> Insert(AutorEntity entity) => await Connection.InsertAsync(entity) > 0;

        public override async Task<bool> Update(AutorEntity entity) => await Connection.UpdateAsync(entity);
    }
}
