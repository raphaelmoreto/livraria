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
                            ValidateIssuer = true, //QUEM GEROU O TOKEN (false = NÃO IMPORTA QUEM GEROU O TOKEN, EU ACEITO.)

                            //PARA QUEM O TOKEN FOI GERADO (false = NÃO VOU VALIDAR PARA QUEM O TOKEN É DESTINADO.)
                            ValidateAudience = true,

                            //VALIDA A ASSINATURA (O SISTEMA SÓ ACEITA TOKENS ASSINADOS COM A SUA CHAVE SECRETA.)
                            ValidateIssuerSigningKey = true, 

                            ValidateLifetime = true, //VALIDA SE O TOKEN AINDA ESTÁ DENTRO DO PERÍODO DE VALIDADE

                            ValidIssuer = configuration["Jwt:Issuer"], //QUAL EMISSOR A API CONSIDERA VÁLIDO?; QUEM GEROU O TOKEN

                            //QUAL AUDIENCE A API DEVE ACEITAR
                            ValidAudience = configuration["Jwt:Audience"],

                            //INFORMA A CHAVE SECRETA USADA PARA ASSINAR O TOKEN
                            IssuerSigningKey = new SymmetricSecurityKey(key),

                            ClockSkew = TimeSpan.Zero //TOKEN EXPIROU = INVÁLIDO NA HORA
                        };
                    }
                );

            return services;
        }
    }
}
