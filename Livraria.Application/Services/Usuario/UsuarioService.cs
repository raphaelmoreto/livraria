using Livraria.Application.Interfaces.Services.Response;
using Livraria.Application.Interfaces.Services.Usuario;
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

        public UsuarioService
        (
            IServiceResponse serviceResponse,
            IUsuarioWriteRepository repositoryUsuario
        ) : base(serviceResponse)
        {
            this.repositoryUsuario = repositoryUsuario;
        }

        public async Task<IServiceResponse> Delete(int id)
        {
            if (id < 0)
            {
                Response.SetError("ID DO USUÁRIO NÃO INFORMADO!");
                return Response;
            }

            var deleteUsuario = await repositoryUsuario.DeletarCadastro(id);
            if (!deleteUsuario)
            {
                Response.SetError($"ERRO! {deleteUsuario}");
                return Response;
            }

            Response.SetSuccess("USUÁRIO DELETADO COM SUCESSO");
            return Response;
        }

        public async Task<IServiceResponse> Insert(UsuarioInputDto dto)
        {
            var idPerfil = await repositoryUsuario.VerificarIdDoPerfil(dto.Fk_Perfil);
            if (!idPerfil)
            {
                Response.SetError("PERFIL DE USUÁRIO NÃO ENCONTRADO");
                return Response;
            }

            var regexEmail = new Regex(@"^[\w.-]+@([\w-]+\.)+[a-zA-Z]{2,}$");
            bool validarEmail = regexEmail.IsMatch(dto.Email);
            if (!validarEmail)
            {
                Response.SetWarning($"EMAIL {dto.Email} FORA DO PADRÃO");
                return Response;
            }

            var usuario = new UsuarioEntity
            (
                dto.Nome,
                dto.Usuario,
                dto.Email,
                dto.Senha,
                dto.Fk_Perfil
            );

            var insert = await repositoryUsuario.Insert(usuario);
            if (!insert)
            {
                Response.SetError($"ERRO! {insert}");
                return Response;
            }

            Response.SetSuccess("USUÁRIO CADASTRADO COM SUCESSO");
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, UsuarioInputDto dto)
        {
            if (id <= 0)
            {
                Response.SetError("ID DO USUÁRIO NÃO INFORMADO!");
                return Response;
            }

            var regexEmail = new Regex(@"^[\w.-]+@[\w-]+\.[a-zA-Z]{2,}$");
            bool validarEmail = regexEmail.IsMatch(dto.Email);
            if (!validarEmail)
            {
                Response.SetWarning($"EMAIL {dto.Email} FORA DO PADRÃO");
                return Response;
            }

            var idPerfil = await repositoryUsuario.VerificarIdDoPerfil(dto.Fk_Perfil);
            if (!idPerfil)
            {
                Response.SetError("PERFIL DE USUÁRIO NÃO ENCONTRADO");
                return Response;
            }

            var usuario = await repositoryUsuario.GetById(id);
            if (usuario == null)
            {
                Response.SetError("USUÁRIO NÃO ENCONTRADO NO BANCO");
                return Response;
            }

            usuario.AtribuirNome(dto.Nome);
            usuario.AtribuirUsuario(dto.Usuario);
            usuario.AtribuirEmail(dto.Email);
            usuario.AtribuirSenha(dto.Senha);
            usuario.AtribuirPerfil(dto.Fk_Perfil);
            usuario.Validar();

            var usuarioAtualizado = await repositoryUsuario.Update(usuario);
            if (!usuarioAtualizado)
            {
                Response.SetError($"ERRO! {usuarioAtualizado}");
                return Response;
            }

            Response.SetSuccess("USUÁRIO ATUALIZADO COM SUCESSO");
            return Response;
        }
    }
}
