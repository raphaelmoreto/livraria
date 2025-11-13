using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.LivroRepository
{
    public class LivroRepository : BaseWrite<LivroEntity>, IBaseWrite<LivroEntity>
    {
        public LivroRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public override async Task<bool> Delete(LivroEntity entity) => await Connection.DeleteAsync(entity);

        public override async Task<LivroEntity> Get(int id) => await Connection.GetAsync<LivroEntity>(id);

        public override async Task<IEnumerable<LivroEntity>> GetAll() => await Connection.GetAllAsync<LivroEntity>();

        public override async Task<bool> Insert(LivroEntity entity) => await Connection.InsertAsync(entity) > 0;

        public override async Task<bool> Update(LivroEntity entity) => await Connection.UpdateAsync(entity);
    }
}
