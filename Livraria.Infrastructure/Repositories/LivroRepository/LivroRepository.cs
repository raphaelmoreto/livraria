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
    }
}
