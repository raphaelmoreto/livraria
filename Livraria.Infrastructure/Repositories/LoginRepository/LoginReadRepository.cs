using Dapper;
using Livraria.Domain.Dtos.Login;
using Livraria.Domain.Interfaces.Repositories.Login;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.LoginRepository
{
    public class LoginReadRepository : BaseRead<LoginInputDto>, ILoginReadRepository
    {
        public LoginReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<LoginOutputDto?> ValidarLogin(LoginInputDto login)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT u.[id],");
            sb.AppendLine("            p.[tipo]");
            sb.AppendLine("FROM [dbo].[Usuario] u");
            sb.AppendLine("INNER JOIN [dbo].[Perfil_Usuario] p ON u.id = p.id");
            sb.AppendLine("WHERE [u].[usuario] = @Usuario");
            sb.AppendLine("AND [u].[senha] = @Senha");
            sb.AppendLine("AND [u].[Ativo] = 1");

            return await Connection.QueryFirstOrDefaultAsync<LoginOutputDto>(sb.ToString(), new { login.Usuario, login.Senha });
        }
    }
}
