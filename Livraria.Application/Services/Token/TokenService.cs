using Livraria.Application.Interfaces.Token;
using Livraria.Domain.Dtos.Login;
using Microsoft.IdentityModel.Tokens; //ASSINA E VALIDA TOKEN
using System.IdentityModel.Tokens.Jwt; //CRIA E LÊ TOKEN
using System.Security.Claims;
using System.Text;

namespace Livraria.Application.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly string Secret; //SENHA SECRETA USADA PARA ASSINAR O TOKEN

        public TokenService(string secret)
        {
            Secret = secret;
        }

        public string GerarToken(LoginDto login)
        {
            //DEFINE COMO OS BYTES DEVEM SER CONVERTIDOS PARA TEXTO E VICE-VERSA
            var key = Encoding.ASCII.GetBytes(Secret);

            //O 'tokenDescriptor' DEFINE COMO O TOKEN SERÁ CRIADO
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //'Subject' INDICA QUEM É O DONO DO TOKEN
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, login.Usuario)
                    }
                ),
                //QUANDO O TOKEN EXPIRA.
                Expires = DateTime.UtcNow.AddMinutes(60),

                //COMO O TOKEN SERÁ ASSINADO
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler(); //É COMO SE FOSSE UM IMPRESSOR DE TOKENS
            var token = tokenHandler.CreateToken(tokenDescriptor); //GERA O TOKEN JWT
            return tokenHandler.WriteToken(token); //TRANFORMA O TOKEN EM TEXTO
        }
    }
}
