using Livraria.API.Controllers.Base;
using Livraria.Application.Interfaces.Token;
using Livraria.Domain.Dtos.Login;
using Livraria.Domain.Interfaces.Repositories.Login;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(loginDto.Usuario) && !string.IsNullOrWhiteSpace(loginDto.Senha))
                {
                    var result = await loginReadRepository.ValidarLogin(loginDto);
                    if (result)
                    {
                        var token = tokenService.GerarToken(loginDto);
                        return Ok(token);
                    }
                }

                return NotFound(new { erro = $"EMAIL/SENHA INVÁLIDOS" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }
    }
}
