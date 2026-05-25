using Dapper.Contrib.Extensions;
using Flunt.Notifications;

namespace Livraria.Domain.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        [Key]
        public int Id { get; protected set; }

        public abstract bool Validar();
    }
}
