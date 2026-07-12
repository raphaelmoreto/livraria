
namespace Livraria.Domain.Dtos.Arquivo
{
    //public class ArquivoDto
    //{
    //    /// <summary>
    //    /// O "init" SIGNIFICA QUE A PROPRIEDADE SÓ PODE SER ATRIBUÍDA DURANTE A CRIAÇÃO DO OBJETO. COM "set" VOCÊ PODE ALTERAR O VALOR A QUALQUER MOMENTO
    //    /// </summary>
    //    public Stream Stream { get; init; } = default!;

    //    public string NomeArquivo { get; init; } = string.Empty;

    //    public string ContentType {  get; init; } = string.Empty;
    //}

    public record ArquivoDto(Stream Stream, string NomeArquivo, string Extensao, string ContentType);
}
