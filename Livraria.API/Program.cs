using Livraria.IoC;
using Microsoft.IdentityModel.Tokens;
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
            builder.Services.AddSwaggerGen();

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

            builder.Services.AddSwaggerGen
            (
                c =>
                {
                    c.AddSecurityDefinition
                    (
                        "Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        { 
                            Name = "Authorization",
                            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                            Scheme = "Bearer",
                            BearerFormat = "JWT",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                            Description = "Digite: Bearer {seu token}"
                        }
                    );

                    c.AddSecurityRequirement
                    (
                        new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
                        {
                            {
                                new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                                {
                                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                            }
                        }
                    );
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
