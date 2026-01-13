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

        public override async Task<bool> Insert(CategoriaLivroEntity categoria)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Categoria] ([nome])");
            sb.AppendLine("SELECT @Nome");
            sb.AppendLine("WHERE NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Categoria] AS categoria");
            sb.AppendLine("         WHERE categoria.nome = @Nome");
            sb.AppendLine(")");

            return await Connection.ExecuteAsync(sb.ToString(), new { categoria.Nome }) > 0;
        }

        public override async Task<bool> Update(CategoriaLivroEntity categoria)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Categoria]");
            sb.AppendLine("SET [nome] = @Nome");
            sb.AppendLine("WHERE [id] = @Id");
            sb.AppendLine("AND NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Categoria] AS categoria");
            sb.AppendLine("         WHERE categoria.nome = @Nome");
            sb.AppendLine(")");

            return await Connection.ExecuteAsync(sb.ToString(), new { categoria.Id, categoria.Nome }) > 0;
        }
    }
}
