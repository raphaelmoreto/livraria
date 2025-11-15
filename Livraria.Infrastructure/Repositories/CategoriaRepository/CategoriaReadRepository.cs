using Dapper;
using Livraria.Domain.Dtos.CategoriaLivro;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.CategoriaRepository
{
    public class CategoriaReadRepository : BaseRead<CategoriaLivroOutputDto>, IRepositoryRead<CategoriaLivroOutputDto>
    {
        public CategoriaReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<IEnumerable<CategoriaLivroOutputDto>> Listar()
        {
            SB.AppendLine("SELECT [id],");
            SB.AppendLine("            [nome]");
            SB.AppendLine("FROM [dbo].[Categoria]");

            return await Connection.QueryAsync<CategoriaLivroOutputDto>(SB.ToString());
        }

        public async Task<CategoriaLivroOutputDto?> SelecionarPorId(int id)
        {
            SB.AppendLine("SELECT [id],");
            SB.AppendLine("            [nome]");
            SB.AppendLine("FROM [dbo].[Categoria]");
            SB.AppendLine("WHERE [id] = @Id");

            return await Connection.QueryFirstOrDefaultAsync<CategoriaLivroOutputDto>(SB.ToString(), new { Id = id });
        }
    }
}
