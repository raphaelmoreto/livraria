
namespace Livraria.Application.Interfaces.Services.Arquivo
{
    public interface ILerArquivo<T> where T : class
    {
        ICriarDados<T> LerArquivo(string extensao, byte[] dados);
    }
}
