
namespace Livraria.Application.Interfaces.Services.Arquivo
{
    public interface ICriarDados<T> where T : class
    {
        Task<List<T>> CriarDados();
    }
}
