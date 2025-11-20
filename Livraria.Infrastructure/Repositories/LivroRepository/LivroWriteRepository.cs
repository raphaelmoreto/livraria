using Dapper;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.LivroRepository
{
    public class LivroWriteRepository : BaseWrite<LivroEntity>, ILivroWriteRepository
    {
        public LivroWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> BuscarLivroPorNome(string nomeLivro, string codigo_barras)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT COUNT(CASE WHEN [titulo] = @Titulo");
            sb.AppendLine("                        AND [codigo_barras] = @Codigo_Barras");
            sb.AppendLine("                        THEN 1 END)");
            sb.AppendLine("FROM [dbo].[Livro]");

            return await Connection.QuerySingleAsync(sb.ToString(), new { Titulo = nomeLivro, Codigo_Barras = codigo_barras});
        }
    }
}
