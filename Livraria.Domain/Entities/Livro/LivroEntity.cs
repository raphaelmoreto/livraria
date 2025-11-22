using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Validations;

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

        public int Autor {  get; private set; }

        public int Categoria { get; private set; }

        public LivroEntity() { }

        public LivroEntity(string titulo, string isbn, DateTime dt_publicacao, decimal preco, int qt_estoque, int categoria, string? subtitulo = null, int? autor = null)
        {
            AtribuirTitulo(titulo);
            AtribuirIsbn(isbn);
            AtribuirDataPublicacao(dt_publicacao);
            AtribuirPreco(preco);
            AtribuirQuantidade(qt_estoque);
            AtribuirCategoria(categoria);
            AtribuirSubtitulo(subtitulo);
            AtribuirAutor(autor);
            Validar();
        }

        public void AtribuirTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                DomainValidationException.AtribuirExcecao("TITULO NÃO PODE SER NULO/VÁZIO");
                return;
            }

            if (titulo == Titulo)
                return;

            Titulo = titulo.ToUpper();
        }

        public void AtribuirSubtitulo(string? subtitulo)
        {
            if (string.IsNullOrWhiteSpace(subtitulo) || subtitulo == Subtitulo)
                return;

            Subtitulo = subtitulo.ToUpper();
        }

        public void AtribuirIsbn(string isbn)
        {
            if (string.IsNullOrWhiteSpace(isbn))
            {
                DomainValidationException.AtribuirExcecao("ISBN NÃO PODE SER NULO/VÁZIO");
                return;
            }

            if (isbn == Isbn)
                return;

            Isbn = isbn;
        }

        public void AtribuirDataPublicacao(DateTime dt_publicacao)
        {
            if (dt_publicacao == null)
            {
                DomainValidationException.AtribuirExcecao("ANO DE PUBLICAÇÃO NÃO PODE SER NULO/VÁZIO");
                return;
            }

            if (dt_publicacao == Dt_Publicacao)
                return;

            Dt_Publicacao = dt_publicacao.Date;
        }

        public void AtribuirPreco(decimal preco)
        {
            if (preco == 0.0m)
            {
                DomainValidationException.AtribuirExcecao("PREÇO DO LIVRO OBRIGATÓRIO");
                return;
            }

            if (preco == Preco)
                return;

            Preco = preco;
        }

        public void AtribuirQuantidade(int qt_estoque)
        {
            if (qt_estoque <= 0)
            {
                Qt_Estoque = 1;
            }

            Qt_Estoque = qt_estoque;
        }

        public void AtribuirAutor(int? autor)
        {
            if (autor <= 0 || autor == Autor)
                return;

            Autor = autor.Value;
        }

        public void AtribuirCategoria(int categoria)
        {
            if (categoria <= 0)
            {
                DomainValidationException.AtribuirExcecao("CATEGORIA DO LIVRO OBRIGATÓRIO");
                return;
            }

            if (categoria == Categoria)
                return;

            Categoria = categoria;
        }

        public void Validar()
        {
            if (DomainValidationException.TemExcecoes())
                throw new AggregateException(DomainValidationException.Excecoes);
        }
    }
}
