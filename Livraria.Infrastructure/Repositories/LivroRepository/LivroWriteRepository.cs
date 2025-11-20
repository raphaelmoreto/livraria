using Dapper;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;

namespace Livraria.Infrastructure.Repositories.LivroRepository
{
    public class LivroWriteRepository : BaseWrite<LivroEntity>, ILivroWriteRepository
    {
        public LivroWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> BuscarLivroPorNome(string nomeLivro, string codigo_barras)
        {
            SB.AppendLine("SELECT COUNT(CASE WHEN [titulo] = @Titulo");
            SB.AppendLine("                        AND [codigo_barras] = @Codigo_Barras");
            SB.AppendLine("                        THEN 1 END)");
            SB.AppendLine("FROM [dbo].[Livro]");

            return await Connection.QuerySingleAsync(SB.ToString(), new { Titulo = nomeLivro, Codigo_Barras = codigo_barras});
        }
    }
}
