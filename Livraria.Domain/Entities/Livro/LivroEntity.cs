using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Validations;

namespace Livraria.Domain.Entities.Livro
{
    public class LivroEntity : BaseEntity
    {
        public string Titulo { get; private set; }

        public string? Subtitulo { get; private set; } = string.Empty;

        public string Isbn { get; private set; }

        public DateTime Dt_Publicacao { get; private set; }

        public string Codigo_Barras { get; private set; }

        public decimal Preco {  get; private set; }

        public string? Autor {  get; private set; }

        public string Categoria { get; private set; }

        public LivroEntity() { }

        public LivroEntity(string titulo, string subtitulo, string isbn, DateTime dt_publicacao, string codigoBarras, decimal preco, string autor, string categoria)
        {
            AtribuirTitulo(titulo);
            AtribuirSubtitulo(subtitulo);
            AtribuirIsbn(isbn);
            AtribuirDataPublicacao(dt_publicacao);
            AtribuirCodigoBarras(codigoBarras);
            AtribuirPreco(preco);
            AtribuirAutor(autor);
            AtribuirCategoria(categoria);
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

        public void AtribuirSubtitulo(string subtitulo)
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

            Dt_Publicacao = dt_publicacao;
        }

        public void AtribuirCodigoBarras(string codigoBarras)
        {
            if (string.IsNullOrWhiteSpace(codigoBarras))
            {
                DomainValidationException.AtribuirExcecao("CÓDIGO DE BARRAS NÃO PODE SER NULO/VÁZIO");
                return;
            }

            if (codigoBarras == Codigo_Barras)
                return;

            Codigo_Barras = codigoBarras;
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

        public void AtribuirAutor(string autor)
        {
            if (string.IsNullOrWhiteSpace(autor) || autor == Autor)
                return;

            Autor = autor.ToUpper();
        }

        public void AtribuirCategoria(string categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria))
            {
                DomainValidationException.AtribuirExcecao("CATEGORIA DO LIVRO OBRIGATÓRIO");
                return;
            }

            if (categoria == Categoria)
                return;

            Categoria = categoria.ToUpper();
        }

        public void Validar()
        {
            if (DomainValidationException.TemExcecoes())
                throw new AggregateException(DomainValidationException.Excecoes);
        }
    }
}
