
namespace Livraria.Application.Interfaces.Services.Arquivo
{
    public interface IGerarArquivo<T> where T : class
    {
        ICriarBytes CriarBytes(string extensao, List<T> dados);
    }
}
