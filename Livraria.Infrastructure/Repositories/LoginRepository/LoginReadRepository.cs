using Dapper;
using Livraria.Domain.Dtos.Login;
using Livraria.Domain.Interfaces.Repositories.Login;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Text;

namespace Livraria.Infrastructure.Repositories.LoginRepository
{
    public class LoginReadRepository : BaseRead<LoginDto>, ILoginReadRepository
    {
        public LoginReadRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<int> ValidarLogin(LoginDto login)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT [id]");
            sb.AppendLine("FROM [dbo].[Usuario]");
            sb.AppendLine("WHERE [Usuario].[usuario] = @Usuario");
            sb.AppendLine("AND [Usuario].[senha] = @Senha");
            sb.AppendLine("AND [Usuario].[Ativo] = 1");

            return await Connection.QuerySingleOrDefaultAsync<int>(sb.ToString(), new { login.Usuario, login.Senha });
        }
    }
}
