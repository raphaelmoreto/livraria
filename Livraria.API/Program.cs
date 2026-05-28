using Livraria.API.Configurations;
using Livraria.API.Middlewares;
using Livraria.IoC;

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

            builder.Services.AddCors
            (
                options =>
                {
                    //CRIA POLÍTICA DE CORS CHAMADA "AllowAngular". ESSA POLÍTICA DEFINE QUEM PODE ACESSAR A API
                    options.AddPolicy("AllowAngular",
                        policy =>
                        {
                            policy
                                .WithOrigins("http://localhost:4200") //PERMITE SOMENTE REQUISIÇÕES VINDAS DO ANGULAR RODANDO NESSA URL
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                        }
                    );
                }
            );

            //CONFIGURAÇÕES DO SWAGGER
            Swagger.AddConfiguracoesSwagger(builder.Services);

            //CONFIGURAÇÕES DE AUTENTICAÇÃO
            Autenticacao.AddConfiguracaoAutenticacao(builder.Services, builder.Configuration);

            DependecyInjectionOfRepositories.AddInfrastructure(builder.Services);
            DependecyInjectionOfServices.AddApplication(builder.Services, builder.Configuration);
            DependecyInjectionOfInfrastructure.AddInfrastructure(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //PIPELINE É O FLUXO DE EXECUÇÃO DA REQUISIÇÃO COMO ABAIXO

            //"UseMiddleware<T>()" ADICIONA UM MIDDLEWARE NO PIPELINE DA APLICAÇÃO
            //<ExceptionMiddleware> MIDDLEWARE QUE SERÁ EXECUTADO
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseHttpsRedirection(); //VERIFICA SE A REQUISIÇÃO VEIO EM HTTP

            app.UseCors("AllowAngular"); //REGISTRA O CORS CONFIGURADO ACIMA NA PIPELINE

            //SEMPRE QUE CHEGAR UMA REQUISIÇÃO, A API IRÁ VERIFICAR O TOKEN JWT, COOKIES E IDENTIFICAR QUEM É O USUÁRIO
            app.UseAuthentication();

            //DEPOIS DE IDENTIFICAR O USUÁRIO, VERIFICA SE TEM PERMISSÃO PARA ACESSAR O ENDPOINT
            app.UseAuthorization(); 

            app.MapControllers();

            app.Run();
        }
    }
}
