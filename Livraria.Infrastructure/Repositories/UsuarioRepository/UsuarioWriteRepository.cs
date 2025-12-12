using Dapper;
using Livraria.Domain.Entities.Usuario;
using Livraria.Domain.Interfaces.Repositories.Usuario;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.UsuarioRepository
{
    public class UsuarioWriteRepository : BaseWrite<UsuarioEntity>, IUsuarioWriteRepository
    {
        public UsuarioWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> DeletarCadastro(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Usuario]");
            sb.AppendLine("SET [dt_exclusao] = @DtExclusao,");
            sb.AppendLine("      [ativo] = @Ativo");
            sb.AppendLine("WHERE id = @Id");

            var param = new
            {
                Id = id,
                DtExclusao = DateTime.Now,
                Ativo = 0
            };

            return await Connection.ExecuteAsync(sb.ToString(), param) > 0;
        }

        public override async Task<bool> Insert(UsuarioEntity usuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Usuario] ([nome], [email], [senha], [dt_cadastro])");
            sb.AppendLine("SELECT @Nome,");
            sb.AppendLine("            @Email,");
            sb.AppendLine("            @Senha,");
            sb.AppendLine("            @DtCadastro");
            sb.AppendLine("WHERE NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Usuario] AS usuario");
            sb.AppendLine("         WHERE usuario.email = @Email");
            sb.AppendLine(")");

            var param = new
            {
                usuario.Nome,
                usuario.Email,
                usuario.Senha,
                DtCadastro = DateTime.Now
            };

            return await Connection.ExecuteAsync(sb.ToString(), param) > 0;
        }

        public override async Task<bool> Update(UsuarioEntity usuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Usuario]");
            sb.AppendLine("SET [nome] = @Nome,");
            sb.AppendLine("       [email] = @Email,");
            sb.AppendLine("       [senha] = @Senha");
            sb.AppendLine("WHERE [id] = @Id");
            sb.AppendLine("AND NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Usuario] AS usuario");
            sb.AppendLine("         WHERE usuario.email = @Email");
            sb.AppendLine(")");

            var param = new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Email,
                usuario.Senha,
            };

            return await Connection.ExecuteAsync(sb.ToString(), param) > 0;
        }
    }
}
