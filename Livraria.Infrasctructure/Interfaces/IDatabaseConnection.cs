using System.Data;

namespace Livraria.Infrasctructure.Interfaces
{
    public interface IDatabaseConnection
    {
        IDbConnection GetConnection();
    }
}
