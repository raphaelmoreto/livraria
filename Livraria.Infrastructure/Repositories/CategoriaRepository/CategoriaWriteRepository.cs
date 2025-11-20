using Dapper;
using Livraria.Domain.Entities.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.CategoriaRepository
{
    public class CategoriaWriteRepository : BaseWrite<CategoriaLivroEntity>, ICategoriaWriteRepository
    {
        public CategoriaWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> BuscarCategoriaPorNome(string nomeCategoria)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COUNT([nome])");
            sb.AppendLine("FROM [dbo].[Categoria]");
            sb.AppendLine("WHERE [nome] = @Nome");

            return await Connection.QuerySingleAsync<int>(sb.ToString(), new { Nome = nomeCategoria }) > 0;
        }
    }
}
