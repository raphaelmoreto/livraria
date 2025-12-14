using Livraria.IoC;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Livraria.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen
            (
                options =>
                {
                    options.AddSecurityDefinition //EXPLICA PARA O SWAGGER O QUE É O TOKEN
                    (
                        "Bearer", new OpenApiSecurityScheme
                        {
                            Name = "Authorization", //NOME DO HEADER HTTP ONDE O TOKEN VAI
                            Type = SecuritySchemeType.Http, //SEGURANÇA VIA PROTOCOLO HTTP
                            Scheme = "Bearer", //TIPO DE AUTENTICAÇÃO
                            BearerFormat = "JWT",  //INFORMA QUE O BEARER É DO TIPO JWT
                            In = ParameterLocation.Header, //INFORMA QUE O TOKEN VEM DO HEADER, NÃO DA URL NEM DO BODY
                            Description = "INSIRA O TOKEN JWT" //TEXTO QUE APARECE DO SWAGGER
                        }
                    );

                    options.AddSecurityRequirement //OBRIGA O SWAGGER A USAR O TOKEN
                    (
                        new OpenApiSecurityRequirement //INFORMA QUE A API EXIGE AUTENTICAÇÃO
                        {
                            {
                                new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                            }
                        }
                    );
                }
            );

            var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"]!);

            // O "Bearer" SERVE PARA A API SABER COMO INTERPRETAR O QUE VEM DEPOIS (O QUE VEM DEPOIS É UM TOKEN JWT)
            builder.Services.AddAuthentication("Bearer")
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

            DependencyInjectionOfApi.AddApi(builder.Services, builder.Configuration);
            DependecyInjectionOfRepositories.AddInfrastructure(builder.Services);
            DependecyInjectionOfServices.AddApplication(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); //SEMPRE QUE CHEGAR UMA REQUISIÇÃO, A API TENTARÁ IDENTIFICAR QUEM É O USUÁRIO
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
