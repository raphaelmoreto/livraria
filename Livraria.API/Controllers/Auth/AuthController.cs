using Livraria.API.Controllers.Base;
using Livraria.Application.Interfaces.Token;
using Livraria.Domain.Dtos.Login;
using Livraria.Domain.Interfaces.Repositories.Login;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Livraria.API.Controllers.Auth
{
    [Route("api/auth")]
    [ApiController]
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
                if (!string.IsNullOrWhiteSpace(loginDto.Email) && !string.IsNullOrWhiteSpace(loginDto.Senha))
                {
                    var result = await loginReadRepository.ValidarLogin(loginDto);
                    if (result)
                    {
                        var token = tokenService.GerarToken(loginDto);
                        return Ok(token);
                    }
                }

                return Unauthorized(JsonSerializer.Serialize(new { erro = $"EMAIL/SENHA INVÁLIDOS" }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"ERRO INTERNO: {ex.Message}");
            }
        }
    }
}
