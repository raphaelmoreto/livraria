
namespace Livraria.Domain.Dtos.Livro
{
    public record LivroInputDto
    (
        string Titulo,
        string Isbn,
        DateTime dtPublicacao,
        string CodigoBarras,
        decimal Preco,
        string Categoria,
        string? Subtitulo = null,
        string? Autor = null
    );

    public record LivroOutputDto
    (
        string Titulo,
        string Isbn,
        DateTime dtPublicacao,
        decimal Preco,
        string Categoria,
        string? Subtitulo = null,
        string? Autor = null
    );
}
