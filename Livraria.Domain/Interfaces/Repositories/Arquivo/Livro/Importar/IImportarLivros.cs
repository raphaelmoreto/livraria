using Livraria.Domain.Dtos.Arquivo;
using Livraria.Domain.Entities.Livro;

namespace Livraria.Domain.Interfaces.Repositories.Arquivo.Livro.Importar
{
    public interface IImportarLivros
    {
        bool SuportaExtensao(string extensao);

        Task <IEnumerable<LivroEntity>> LerArquivo(ArquivoDto dto);
    }
}
