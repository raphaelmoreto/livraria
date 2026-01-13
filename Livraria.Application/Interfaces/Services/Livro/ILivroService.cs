using Livraria.Application.Interfaces.Services.Base;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Domain.Dtos.Livro;

namespace Livraria.Application.Interfaces.Services.Livro
{
    public interface ILivroService : IUpdate<LivroInputDto>
    {
        Task<byte[]> DownloadLivros(string extensao);

        Task<IServiceResponse> Insert(LivroInputDto dto, string usuarioLogado);

        Task<IServiceResponse> RemoverCategorias(int idLivro, List<int> categorias);
    }
}
