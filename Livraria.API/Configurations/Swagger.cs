using Microsoft.OpenApi.Models;

namespace Livraria.API.Configurations
{
    public static class Swagger
    {
        public static IServiceCollection AddConfiguracoesSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen
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

            return services;
        }
    }
}
