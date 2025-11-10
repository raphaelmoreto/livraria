using System.Data;

namespace Livraria.Infrastructure.Interfaces
{
    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}
