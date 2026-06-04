namespace Livraria.API.Middlewares
{
    /// <summary>
    /// "Middlewares" SÃO COMPONENTES QUE FICAM NO PIPELINE DE REQUISIÇÕES DA API. ELAS INTERCEPTAM TODA REQUISIÇÃO HTTP ANTES DELA CHEGAR NO "Controllers" E, TAMBÉM, PODEM INTERCEPTAR A RESPOSTA ANTES DELA VOLTAR PARA O CLIENTE. É UMA CAMADA DE SOFTWARE QUE ATUA COMO UMA "PONTE" ENTRE DIFERENTES APLICAÇÕES
    /// 
    /// O QUE UM "Middleware" FAZ?
    /// • VALIDAR AUTENTICAÇÃO;
    /// • TRATAR ERROS;
    /// • REGISTRAR LOGS
    /// • MANIPULAR HEADERS
    /// • FAZER CACHE
    /// • MEDIR TEMPO DE EXECUÇÃO
    /// • BLOQUEAR REQUISIÇÕES
    /// • REDIRECIONAR CHAMADAS
    /// • VALIDAR CORS
    /// • COMPRIMIR RESPOSTAS
    /// </summary>

    public class ExceptionMiddleware
    {
        /// <summary>
        /// DELEGATE É UMA VARIÁVEL QUE GUARDA MÉTODOS. É COMO SE FOSSE UM PONTEIRO QUE PARA UMA FUNÇÃO
        /// </summary>

        //"RequestDelegate" É UM TIPO DE DELEGATE DO ASP.NET Core
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) 
        {
            _next = next;
        }

        //O MÉTODO "InvokeAsync" É CHAMADO EM TODA REQUISIÇÃO HTTP QUE PASSA PELA PIPELINE DO ASP.NET Core, DESDE QUE O MIDDLEWARE TENHA SIDO REGISTRADO NO Program.cs
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //EXECUTA O PRÓXIMO MIDDLEWARE CONFIGURADO NA PIPELINE CASO HOUVER
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                //DEFINE O TIPO DE RESPOSTA DA API. ELA IRÁ RETORNAR SEMPRE UM JSON
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new { success = false, erro = ex.Message });
            }
        }
    }
}
