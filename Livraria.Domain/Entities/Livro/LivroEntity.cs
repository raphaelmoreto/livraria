using Dapper.Contrib.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using Livraria.Domain.Entities.Base;

namespace Livraria.Domain.Entities.Livro
{
    [Table("[dbo].[Livro]")]
    public class LivroEntity : BaseEntity
    {
        public string Titulo { get; private set; }

        public string? Subtitulo { get; private set; } = string.Empty;

        public string Isbn { get; private set; }

        public DateTime Dt_Publicacao { get; private set; }

        public decimal Preco {  get; private set; }

        public int Qt_Estoque { get; private set; }

        public int Fk_Autor {  get; private set; }

        public List<int> Fk_Categoria { get; private set; } = [];

        public LivroEntity() { }

        public LivroEntity(string titulo, string isbn, DateTime dt_publicacao, decimal preco, int qt_estoque, List<int> fk_categoria, string? subtitulo = null, int? fk_autor = null)
        {
            AtribuirTitulo(titulo);
            AtribuirIsbn(isbn);
            AtribuirDataPublicacao(dt_publicacao);
            AtribuirPreco(preco);
            AtribuirQuantidade(qt_estoque);
            AtribuirCategoria(fk_categoria);
            AtribuirSubtitulo(subtitulo);
            AtribuirAutor(fk_autor);
        }

        public void AtribuirTitulo(string titulo)
        {
            if (titulo == Titulo)
                return;

            Titulo = titulo?.Trim().ToUpper() ?? string.Empty;
        }

        public void AtribuirSubtitulo(string? subtitulo)
        {
            if (string.IsNullOrWhiteSpace(subtitulo))
            {
                Subtitulo = null;
                return;
            }

            if (subtitulo == Subtitulo)
                return;

            Subtitulo = subtitulo.ToUpper();
        }

        public void AtribuirIsbn(string isbn)
        {
            if (isbn == Isbn)
                return;

            Isbn = isbn.Trim() ?? string.Empty;
        }

        public void AtribuirDataPublicacao(DateTime dt_publicacao)
        {
            if (dt_publicacao == Dt_Publicacao)
                return;

            Dt_Publicacao = dt_publicacao.Date;
        }

        public void AtribuirPreco(decimal preco)
        {
            if (preco == Preco)
                return;

            Preco = preco;
        }

        public void AtribuirQuantidade(int qt_estoque)
        {
            if (qt_estoque <= 0)
            {
                Qt_Estoque = 1;
                return;
            }

            Qt_Estoque = qt_estoque;
        }

        public void AtribuirAutor(int? fk_autor)
        {
            if (fk_autor <= 0 || fk_autor == Fk_Autor)
                return;

            Fk_Autor = fk_autor.Value;
        }

        public void AtribuirCategoria(List<int> fk_categoria)
        {
            if (fk_categoria == Fk_Categoria)
                return;

            Fk_Categoria = fk_categoria;
        }

        public override bool Validar()
        {
            Clear();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Titulo, "Título", "TÍTULO DO LIVRO NÃO PODE SER NULO/VÁZIO")
                .IsNotNullOrWhiteSpace(Isbn, "Isbn", "ISBN DO LIVRO NÃO PODE SER NULO/VÁZIO")
                .AreNotEquals(Dt_Publicacao, DateTime.MinValue, "Data de pubicação", "ANO DE PUBLICAÇÃO NÃO PODE SER NULO/VÁZIO")
                .IsGreaterThan(Preco, 0.0m, "Preço",  "PREÇO DO LIVRO OBRIGATÓRIO")
                .IsTrue(Fk_Categoria.Any(), "Categoria", "CATEGORIA DO LIVRO OBRIGATÓRIO")
            );

            return IsValid;
        }
    }
}
