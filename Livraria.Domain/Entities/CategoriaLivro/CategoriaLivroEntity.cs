using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Validations;

namespace Livraria.Domain.Entities.CategoriaLivro
{
    [Table("[dbo].[Categoria]")]
    public class CategoriaLivroEntity : BaseEntity
    {
        public string Nome {  get; private set; }

        public CategoriaLivroEntity() { }

        public CategoriaLivroEntity(string nome)
        {
            AtribuirNome(nome);
            Validar();
        }

        public void AtribuirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                DomainValidationException.AtribuirExcecao("NOME DA CATEGORIA NÃO PODE SER NULO/VÁZIO");
                return;
            }

            if (nome == Nome)
                return;

            Nome = nome.ToUpper();
        }

        public override void Validar()
        {
            if (DomainValidationException.TemExcecoes())
                throw new AggregateException(DomainValidationException.Excecoes);
        }
    }
}
