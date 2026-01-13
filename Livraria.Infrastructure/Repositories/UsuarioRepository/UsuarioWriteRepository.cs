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
            sb.AppendLine("SET [dt_exclusao] = @Dt_Exclusao,");
            sb.AppendLine("      [ativo] = 0");
            sb.AppendLine("WHERE id = @Id");

            var param = new
            {
                Id = id,
                Dt_Exclusao = DateTime.Now
            };

            return await Connection.ExecuteAsync(sb.ToString(), param) > 0;
        }

        public override async Task<bool> Insert(UsuarioEntity usuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Usuario] ([nome], [usuario], [email], [senha], [dt_cadastro], [fk_perfil])");
            sb.AppendLine("SELECT @Nome,");
            sb.AppendLine("            @Usuario,");
            sb.AppendLine("            @Email,");
            sb.AppendLine("            @Senha,");
            sb.AppendLine("            @DtCadastro,");
            sb.AppendLine("            @FK_Perfil");
            sb.AppendLine("WHERE NOT EXISTS (");
            sb.AppendLine("         SELECT [id]");
            sb.AppendLine("         FROM [dbo].[Usuario] AS usuario");
            sb.AppendLine("         WHERE usuario.email = @Email");
            sb.AppendLine("         AND usuario.usuario = @Usuario");
            sb.AppendLine(")");

            var param = new
            {
                usuario.Nome,
                usuario.Usuario,
                usuario.Email,
                usuario.Senha,
                DtCadastro = DateTime.Now,
                usuario.Fk_Perfil
            };

            return await Connection.ExecuteAsync(sb.ToString(), param) > 0;
        }

        public override async Task<bool> Update(UsuarioEntity usuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Usuario]");
            sb.AppendLine("SET [nome] = @Nome,");
            sb.AppendLine("       [usuario] = @Usuario,");
            sb.AppendLine("       [email] = @Email,");
            sb.AppendLine("       [senha] = @Senha,");
            sb.AppendLine("       [fk_perfil] = @Fk_Perfil");
            sb.AppendLine("WHERE [id] = @Id");

            var param = new
            {
                usuario.Id,
                usuario.Nome,
                usuario.Usuario,
                usuario.Email,
                usuario.Senha,
                usuario.Fk_Perfil
            };

            return await Connection.ExecuteAsync(sb.ToString(), param) > 0;
        }

        public async Task<bool> VerificarIdDoPerfil(int idPerfil)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("     CASE");
            sb.AppendLine("             WHEN COUNT([Perfil_Usuario].[id]) > 0 THEN 1");
            sb.AppendLine("             ELSE 0");
            sb.AppendLine("     END");
            sb.AppendLine("FROM [dbo].[Perfil_Usuario]");
            sb.AppendLine("WHERE [Perfil_Usuario].[id] = @Id");

            return await Connection.QuerySingleAsync<int>(sb.ToString(), new { Id = idPerfil }) > 0;
        }
    }
}
