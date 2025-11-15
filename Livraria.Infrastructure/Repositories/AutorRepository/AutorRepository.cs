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
    }
}
