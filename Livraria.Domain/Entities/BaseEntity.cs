using Dapper.Contrib.Extensions;

namespace Livraria.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; protected set; }
    }
}
