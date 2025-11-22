
namespace Livraria.Domain.Dtos.Livro
{
    public record LivroInputDto
    (
        string Titulo,
        string Isbn,
        DateTime dtPublicacao,
        decimal Preco,
        string Categoria,
        int Quantidade,
        string? Subtitulo = null,
        string? Autor = null
    );

    public record LivroOutputDto
    (
        int Id,
        string Titulo,
        string Isbn,
        DateTime dt_Publicacao,
        decimal Preco,
        int Quantidade,
        string Categoria,
        string? Subtitulo = null,
        string? Autor = null
    );
}
