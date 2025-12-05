
namespace Livraria.Domain.Dtos.Livro
{
    public record LivroInputDto
    (
        string Titulo,
        string Isbn,
        DateTime Dt_Publicacao,
        decimal Preco,
        int Categoria,
        int Quantidade,
        string? Subtitulo = null,
        int? Autor = null
    );

    public record LivroOutputDto
    (
        int Id,
        string Titulo,
        string Isbn,
        DateTime Dt_Publicacao,
        decimal Preco,
        int Quantidade,
        string Categoria,
        string? Subtitulo = null,
        string? Autor = null
    );
}
