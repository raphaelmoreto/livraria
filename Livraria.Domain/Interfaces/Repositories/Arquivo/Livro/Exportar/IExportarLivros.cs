using Livraria.Domain.Dtos.Livro;

namespace Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Exportar
{
    public interface IExportarLivros
    {
        bool SuportaExtensao(string extensao);

        byte[] CriarBytes(List<LivroOutputDto> dados);
    }
}
