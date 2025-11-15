using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories._base;

namespace Livraria.Infrastructure.Repositories.Base
{
    public abstract class BaseRead<T> : BaseRepository
    {
        protected BaseRead(IDatabaseConnection dbConnection) : base(dbConnection) { }
    }
}
