using Livraria.Application.Interfaces.Services.Token;
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

        private readonly string Issuer;

        private readonly string Audience;

        public TokenService(string secret, string issuer, string audience)
        {
            Secret = secret;
            Issuer = issuer;
            Audience = audience;
        }

        public string GerarToken(LoginOutputDto usuario)
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
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Name, usuario.Nome),
                        new Claim(ClaimTypes.Role, usuario.Role)
                    }
                ),
                //QUANDO O TOKEN EXPIRA.
                Expires = DateTime.UtcNow.AddMinutes(90),

                Issuer = this.Issuer,

                Audience = this.Audience,

                //COMO O TOKEN SERÁ ASSINADO
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler(); //É COMO SE FOSSE UM IMPRESSOR DE TOKENS
            var token = tokenHandler.CreateToken(tokenDescriptor); //GERA O TOKEN JWT
            return tokenHandler.WriteToken(token); //TRANFORMA O TOKEN EM TEXTO
        }
    }
}
