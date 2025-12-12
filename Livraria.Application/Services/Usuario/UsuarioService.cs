using Livraria.Application.Interfaces.Response;
using Livraria.Application.Interfaces.Usuario;
using Livraria.Application.Services.Base;
using Livraria.Domain.Dtos.Usuario;
using Livraria.Domain.Entities.Usuario;
using Livraria.Domain.Interfaces.Repositories.Usuario;

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
                Response.Mensagem = "ID DO USUÁRIO NÃO INFORMADO!";
                return Response;
            }

            var deleteUsuario = await repositoryUsuario.DeletarCadastro(id);
            if (!deleteUsuario)
            {
                Response.Mensagem = $"ERRO! {deleteUsuario}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "USUÁRIO DELETADO COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> Insert(UsuarioInputDto dto)
        {
            var usuario = new UsuarioEntity
            (
                dto.Nome,
                dto.Email,
                dto.Senha
            );

            var insert = await repositoryUsuario.Insert(usuario);
            if (!insert)
            {
                Response.Mensagem = $"ERRO! {insert}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "USUÁRIO CADASTRADO COM SUCESSO";
            return Response;
        }

        public async Task<IServiceResponse> Update(int id, UsuarioInputDto dto)
        {
            if (id <= 0)
            {
                Response.Mensagem = "ID DO USUÁRIO NÃO INFORMADO!";
                return Response;
            }

            var usuario = await repositoryUsuario.Get(id);
            if (usuario == null)
            {
                Response.Mensagem = "USUÁRIO NÃO ENCONTRADO NO BANCO";
                return Response;
            }

            usuario.AtribuirNome(dto.Nome);
            usuario.AtribuirEmail(dto.Email);
            usuario.AtribuirSenha(dto.Senha);
            usuario.Validar();

            var usuarioAtualizado = await repositoryUsuario.Update(usuario);
            if (!usuarioAtualizado)
            {
                Response.Mensagem = $"ERRO! {usuarioAtualizado}";
                return Response;
            }

            Response.Success = true;
            Response.Mensagem = "USUÁRIO ATUALIZADO COM SUCESSO";
            return Response;
        }
    }
}
