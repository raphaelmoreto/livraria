using Livraria.Application.Enum.Response;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Interfaces.Services.Usuario;
using Livraria.Application.Response;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Usuario;
using Livraria.Domain.Entities.Usuario;
using Livraria.Domain.Interfaces.Repositories.Usuario;
using System.Text.RegularExpressions;

namespace Livraria.Application.Services.Usuario
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioWriteRepository repositoryUsuario;

        public UsuarioService(IUsuarioWriteRepository repositoryUsuario)
        {
            this.repositoryUsuario = repositoryUsuario;
        }

        public async Task<IServiceResponse> Delete(int id)
        {
            if (id < 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "ID DO USUÁRIO NÃO INFORMADO!");

            var deleteUsuario = await repositoryUsuario.DeletarCadastro(id);
            if (!deleteUsuario)
                return ServiceResponse.Error(TipoRetorno.Conflict, "USUÁRIO NÃO PODE SER DELETADO");

            return ServiceResponse.Ok("USUÁRIO DELETADO COM SUCESSO");
        }

        public async Task<IServiceResponse> Insert(UsuarioInputDto dto)
        {
            var idPerfil = await repositoryUsuario.VerificarIdDoPerfil(dto.Fk_Perfil);
            if (!idPerfil)
                return ServiceResponse.Error(TipoRetorno.NotFound, "PERFIL DE USUÁRIO NÃO ENCONTRADO");

            var regexEmail = new Regex(@"^[\w.-]+@([\w-]+\.)+[a-zA-Z]{2,}$");
            bool validarEmail = regexEmail.IsMatch(dto.Email);
            if (!validarEmail)
                return ServiceResponse.Error(TipoRetorno.BadRequest, $"EMAIL {dto.Email} FORA DO PADRÃO");

            var usuario = new UsuarioEntity
            (
                dto.Nome,
                dto.Usuario,
                dto.Email,
                dto.Senha,
                dto.Fk_Perfil
            );
            if (!usuario.Validar())
                return ServiceResponse.Error(TipoRetorno.Validation, "ERRO DE VALIDAÇÃO", usuario.Notifications.Select(x => x.Message));

            var insert = await repositoryUsuario.Insert(usuario);
            if (!insert)
                return ServiceResponse.Error(TipoRetorno.Conflict, $"ERRO! USUÁRIO NÃO PODE SER CADASTRADO");

            return ServiceResponse.Ok("USUÁRIO CADASTRADO COM SUCESSO");
        }

        public async Task<IServiceResponse> Update(int id, UsuarioInputDto dto)
        {
            if (id <= 0)
                return ServiceResponse.Error(TipoRetorno.BadRequest, "ID DO USUÁRIO NÃO INFORMADO!");

            var regexEmail = new Regex(@"^[\w.-]+@[\w-]+\.[a-zA-Z]{2,}$");
            bool validarEmail = regexEmail.IsMatch(dto.Email);
            if (!validarEmail)
                return ServiceResponse.Error(TipoRetorno.BadRequest, $"EMAIL {dto.Email} FORA DO PADRÃO");

            var idPerfil = await repositoryUsuario.VerificarIdDoPerfil(dto.Fk_Perfil);
            if (!idPerfil)
                return ServiceResponse.Error(TipoRetorno.NotFound, "PERFIL DE USUÁRIO NÃO ENCONTRADO");

            var usuario = await repositoryUsuario.GetById(id);
            if (usuario == null)
                return ServiceResponse.Error(TipoRetorno.NotFound, "USUÁRIO NÃO ENCONTRADO NO BANCO");

            usuario.AtribuirNome(dto.Nome);
            usuario.AtribuirUsuario(dto.Usuario);
            usuario.AtribuirEmail(dto.Email);
            usuario.AtribuirSenha(dto.Senha);
            usuario.AtribuirPerfil(dto.Fk_Perfil);
            if (!usuario.Validar())
                return ServiceResponse.Error(TipoRetorno.Validation, "ERRO DE VALIDAÇÃO", usuario.Notifications.Select(x => x.Message));

            var usuarioAtualizado = await repositoryUsuario.Update(usuario);
            if (!usuarioAtualizado)
                return ServiceResponse.Error(TipoRetorno.Conflict, $"ERRO! USUÁRIO NÃO PODE SER CADASTRADO");

            return ServiceResponse.Ok("USUÁRIO ATUALIZADO COM SUCESSO");
        }
    }
}
