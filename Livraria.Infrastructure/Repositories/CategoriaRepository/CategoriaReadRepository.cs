using Dapper;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories.CategoriaLivro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.CategoriaRepository
{
    public class CategoriaReadRepository : BaseRead<CategoriaLivroOutputDto>, ICategoriaReadRepository
    {
        public CategoriaReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<IEnumerable<CategoriaLivroOutputDto>> Listar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Categoria]");

            return await Connection.QueryAsync<CategoriaLivroOutputDto>(sb.ToString());
        }

        public async Task<CategoriaLivroOutputDto?> SelecionarPorId(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id],");
            sb.AppendLine("            [nome]");
            sb.AppendLine("FROM [dbo].[Categoria]");
            sb.AppendLine("WHERE [id] = @Id");

            return await Connection.QueryFirstOrDefaultAsync<CategoriaLivroOutputDto>(sb.ToString(), new { Id = id });
        }
    }
}
