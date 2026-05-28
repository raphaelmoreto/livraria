using Livraria.API.Controllers.Base;
using Livraria.Application.Interfaces.Services.Token;
using Livraria.Domain.Dtos.Login;
using Livraria.Domain.Interfaces.Repositories.Login;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.API.Controllers.Auth
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private readonly ILoginReadRepository loginReadRepository;

        private readonly ITokenService tokenService;

        public AuthController (ILoginReadRepository loginReadRepository, ITokenService tokenService)
        {
            this.loginReadRepository = loginReadRepository;
            this.tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginInputDto loginDto)
        {
            if (string.IsNullOrWhiteSpace(loginDto.Usuario) && string.IsNullOrWhiteSpace(loginDto.Senha))
                return Erro("USUÁRIO E SENHA SÃO OBRIGATÓRIOS");

            var usuario = await loginReadRepository.ValidarLogin(loginDto);
            if (usuario is null)
                return NaoAutorizado("USUÁRIO OU SENHA INVÁLIDOS!");

            var token = tokenService.GerarToken(usuario);
            return Sucesso( new { token, usuario });
        }
    }
}
