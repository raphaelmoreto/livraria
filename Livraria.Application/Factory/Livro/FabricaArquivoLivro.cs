using Livraria.Application.Arquivo.Exportar.Livro;
using Livraria.Application.Interfaces.Services.Arquivo;
using Livraria.Domain.Dtos.Livro;

namespace Livraria.Application.Factory.Livro
{
    public class FabricaArquivoLivro : IGerarArquivo<LivroOutputDto>, ILerArquivo<LivroInputDto>
    {
        public ICriarBytes CriarBytes(string extensao, List<LivroOutputDto> dados)
        {
            return extensao switch
            {
                ".xlsx" => new LivroXlsx(dados),
                _ => throw new ArgumentException("EXTENSÃO NÃO SUPORTADA!")
            };
        }

        public ICriarDados<LivroInputDto> LerArquivo(string extensao, byte[] dados)
        {
            throw new NotImplementedException();
        }
    }
}
