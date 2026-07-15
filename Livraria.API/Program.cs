using Livraria.API.Configurations;
using Livraria.API.Middlewares;
using Livraria.IoC;
using Serilog;
using System.Text.Json.Serialization;

namespace Livraria.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() //DEFINE O MÍNIMO DE LOG QUE SERÁ GRAVADO (COMO DEFINIU "Information" SERÃO GRAVADOS: Information, Warning, Error, Fatal)
                .WriteTo.File //INDICA QUE O DESTINO ("Sink") DOS LOGS SERÁ UM ARQUIVO (Console, Arquivo, SQL Server, etc...)
                (
                    @"C:\TEMP\LIVRARIA\LOGS\livraria-logs.txt",
                    rollingInterval: RollingInterval.Infinite, //DEFINE QUANDO UM ARQUIVO NOVO SERÁ CRIADO. MAS ASSIM UTILIZARÁ SEMPRE O MESMO ARQUIVO
                    shared: true //PERMITE QUE O ARQUIVO SEJA COMPARTILHADO
                )
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);

            //EM VEZ DE UTILIZAR O SISTEMA PADRÃO DE LOGS (Microsoft.Extensions.Logging) UTILIZARÁ O SERILOG
            builder.Host.UseSerilog();

            // Add services to the container.

            //"AddJsonOptions" CONFIGURA O SERIALIZADOR JSON DA APLPICAÇÃO ASP.NET Core PARA TRATAR enum COMO TEXTO EM VEZ DE NÚMERO
            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                 {
                     options.JsonSerializerOptions.Converters
                         .Add(new JsonStringEnumConverter());
                 });

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

            DependecyInjectionOfInfrastructure.ConfigurarInfrastructure(builder.Services);
            DependecyInjectionOfServices.ConfigurarApplication(builder.Services, builder.Configuration);

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
