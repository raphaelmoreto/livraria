using Livraria.Application.Interfaces.Services.Base;
using Livraria.Application.Interfaces.Services.Response;
using Livraria.Domain.Dtos.Livro;

namespace Livraria.Application.Interfaces.Services.Livro
{
    public interface ILivroService : IDelete, IUpdate<LivroInputDto>
    {
        Task<IServiceResponse> Insert(LivroInputDto dto, string usuarioLogado);
    }
}
