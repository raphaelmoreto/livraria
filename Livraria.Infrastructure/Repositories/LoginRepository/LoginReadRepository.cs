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

        public Task<IEnumerable<LoginDto>> Listar()
        {
            throw new NotImplementedException();
        }

        public Task<LoginDto?> SelecionarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidarLogin(LoginDto login)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("       CASE");
            sb.AppendLine("               WHEN COUNT(*) > 0 THEN 1");
            sb.AppendLine("               ELSE 0");
            sb.AppendLine("       END");
            sb.AppendLine("FROM [dbo].[Usuario]");
            sb.AppendLine("WHERE [Usuario].[usuario] = @Usuario");
            sb.AppendLine("AND [Usuario].[senha] = @Senha");
            sb.AppendLine("AND [Usuario].[Ativo] = 1");

            return await Connection.QuerySingleAsync<int>(sb.ToString(), new { login.Usuario, login.Senha }) > 0;
        }
    }
}
