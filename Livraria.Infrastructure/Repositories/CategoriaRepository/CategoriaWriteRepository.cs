using Dapper;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.CategoriaRepository
{
    public class CategoriaWriteRepository : BaseWrite<CategoriaLivroEntity>, ICategoriaWriteRepository
    {
        public CategoriaWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> BuscarCategoriaPorNome(string nomeCategoria)
        {
            SB.AppendLine("SELECT COUNT([nome])");
            SB.AppendLine("FROM [dbo].[Categoria]");
            SB.AppendLine("WHERE [nome] = @Nome");

            return await Connection.QuerySingleAsync<int>(SB.ToString(), new { Nome = nomeCategoria }) > 0;
        }
    }
}
