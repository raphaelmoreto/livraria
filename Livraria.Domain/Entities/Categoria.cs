using Dapper.Contrib.Extensions;
using Livraria.Domain.Validations;

namespace Livraria.Domain.Entities
{
    [Table("[dbo].[Categoria]")]
    public class Categoria : BaseEntity
    {
        public string Nome {  get; private set; }

        public Categoria() { }

        public Categoria(string nome)
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

        public void Validar()
        {
            if (DomainValidationException.TemExcecoes())
                throw new AggregateException(DomainValidationException.Excecoes);
        }
    }
}
