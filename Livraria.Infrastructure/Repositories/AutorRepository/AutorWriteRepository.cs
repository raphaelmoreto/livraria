using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.AutorRepositories
{
    public class AutorWriteRepository : BaseWrite<AutorEntity>, IRepositoryWrite<AutorEntity>
    {
        public AutorWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }
    }
}
