using Dapper.Contrib.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using Livraria.Domain.Entities.Base;

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
        }

        public void AtribuirNome(string nome)
        {
            if (nome == Nome)
                return;

            Nome = nome?.Trim().ToUpper() ?? string.Empty;
        }

        public override bool Validar()
        {
            Clear();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, "Nome", "NOME DO AUTOR NÃO PODE SER NULO/VÁZIO")
            );

            return IsValid;
        }
    }
}
