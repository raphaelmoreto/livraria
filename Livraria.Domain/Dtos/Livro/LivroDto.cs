
namespace Livraria.Domain.Dtos.Livro
{
    public record LivroInputDto
    (
        string Titulo,
        string Isbn,
        DateTime Dt_Publicacao,
        decimal Preco,
        List<int> Fk_Categoria,
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
        string Categorias,
        string? Subtitulo = null,
        string? Autor = null
    );
}
