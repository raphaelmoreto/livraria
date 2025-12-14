using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Livraria.API.Configurations
{
    public static class Autenticacao
    {
        public static IServiceCollection AddConfiguracaoAutenticacao(this IServiceCollection services, IConfiguration configuration)
        {
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]!);

            // O "Bearer" SERVE PARA A API SABER COMO INTERPRETAR O QUE VEM DEPOIS (O QUE VEM DEPOIS É UM TOKEN JWT)
            services.AddAuthentication("Bearer")
                .AddJwtBearer //CONFIGURA COMO VALIDAR O TOKEN
                (
                    "Bearer", options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = false, //QUEM GEROU O TOKEN (false = NÃO IMPORTA QUEM GEROU O TOKEN, EU ACEITO.)
                            ValidateAudience = false, //PARA QUEM O TOKEN FOI GERADO (false = NÃO VOU VALIDAR PARA QUEM O TOKEN É DESTINADO.)
                            ValidateIssuerSigningKey = true, //VALIDA A ASSINATURA (O SISTEMA SÓ ACEITA TOKENS ASSINADOS COM A SUA CHAVE SECRETA.)
                            IssuerSigningKey = new SymmetricSecurityKey(key), //INFORMA A CHAVE SECRETA USADA PARA ASSINAR O TOKEN
                            ClockSkew = TimeSpan.Zero //TOKEN EXPIROU = INVÁLIDO NA HORA
                        };
                    }
                );

            return services;
        }
    }
}
