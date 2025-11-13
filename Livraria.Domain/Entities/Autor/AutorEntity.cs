using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Validations;

namespace Livraria.Domain.Entities.Autor
{
    [Table("[dbo].[Autor]")]
    public class AutorEntity : BaseEntity
    {
        public string Nome {  get; private set; }

        public AutorEntity() { }

        public AutorEntity(string nome)
        {
            AtribuirNome(nome);
            Validar();
        }

        public void AtribuirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                DomainValidationException.AtribuirExcecao("NOME DO AUTOR NÃO PODE SER NULO/VÁZIO");
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
