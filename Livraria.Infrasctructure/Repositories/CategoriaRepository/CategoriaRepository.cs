using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrasctructure.Interfaces;
using Livraria.Infrasctructure.Repositories.Base;

namespace Livraria.Infrasctructure.Repositories.CategoriaRepository
{
    public class CategoriaRepository : BaseWrite<CategoriaLivroEntity>, IBaseWrite<CategoriaLivroEntity>
    {
        public CategoriaRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public override async Task<bool> Delete(CategoriaLivroEntity entity) => await Connection.DeleteAsync(entity);

        public override async Task<CategoriaLivroEntity> Get(int id) => await Connection.GetAsync<CategoriaLivroEntity>(id);

        public override async Task<IEnumerable<CategoriaLivroEntity>> GetAll() => await Connection.GetAllAsync<CategoriaLivroEntity>();

        public override async Task<bool> Insert(CategoriaLivroEntity entity) => await Connection.InsertAsync(entity) > 0;

        public override async Task<bool> Update(CategoriaLivroEntity entity) => await Connection.UpdateAsync(entity);
    }
}
