using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.CategoriaRepository
{
    public class CategoriaRepository : BaseWrite<CategoriaLivroEntity>, IBaseWrite<CategoriaLivroEntity>
    {
        public CategoriaRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }
    }
}
