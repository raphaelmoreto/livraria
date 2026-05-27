using Dapper.Contrib.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using Livraria.Domain.Entities.Base;

namespace Livraria.Domain.Entities.CategoriaLivro
{
    [Table("[dbo].[Categoria]")]
    public class CategoriaLivroEntity : BaseEntity
    {
        public string Nome {  get; private set; }

        private CategoriaLivroEntity() { }

        public CategoriaLivroEntity(string nome)
        {
            AtribuirNome(nome);
        }

        public void AtribuirNome(string nome)
        {
            if (nome == Nome)
                return;

            Nome = nome.Trim().ToUpper() ?? string.Empty;
        }

        public override bool Validar()
        {
            Clear();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, "Nome", "NOME DA CATEGORIA NÃO PODE SER NULO/VÁZIO")
            );

            return IsValid;
        }
    }
}
