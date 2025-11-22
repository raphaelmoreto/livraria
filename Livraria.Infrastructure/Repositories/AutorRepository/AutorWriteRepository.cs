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

        //public async Task<bool> BuscarAutorPorNome(string nomeAutor)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("SELECT COUNT([nome])");
        //    sb.AppendLine("FROM [dbo].[Autor]");
        //    sb.AppendLine("WHERE [nome] = @Nome");

        //    return await Connection.QuerySingleAsync<int>(sb.ToString(), new { Nome = nomeAutor }) > 0;
        //}

        public override async Task<bool> Insert(AutorEntity autor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Autor] ([nome])");
            sb.AppendLine("SELECT @Nome");
            sb.AppendLine("WHERE NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Autor] AS autor");
            sb.AppendLine("         WHERE autor.nome = @Nome");
            sb.AppendLine(")");

            return await Connection.ExecuteAsync(sb.ToString(), new { autor.Nome }) > 0;
        }

        public override async Task<bool> Update(AutorEntity autor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Autor]");
            sb.AppendLine("SET [nome] = @Nome");
            sb.AppendLine("WHERE [id] = @Id");
            sb.AppendLine("AND NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Autor] AS autor");
            sb.AppendLine("         WHERE autor.nome = @Nome");
            sb.AppendLine(")");

            return await Connection.ExecuteAsync(sb.ToString(), new { autor.Id,  autor.Nome }) > 0;
        }
    }
}
