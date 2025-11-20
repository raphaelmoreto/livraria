using Dapper;
using Livraria.Domain.Entities.Autor;
using Livraria.Domain.Interfaces.Repositories.Autor;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.AutorRepository
{
    public class AutorWriteRepository : BaseWrite<AutorEntity>, IAutorWriteRepository
    {
        public AutorWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> BuscarAutorPorNome(string nomeAutor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COUNT([nome])");
            sb.AppendLine("FROM [dbo].[Autor]");
            sb.AppendLine("WHERE [nome] = @Nome");

            return await Connection.QuerySingleAsync<int>(sb.ToString(), new { Nome = nomeAutor }) > 0;
        }
    }
}
