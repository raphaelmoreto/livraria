
namespace Livraria.Application.Arquivo.Base
{
    public abstract class BaseImportar<T>
    {
        protected byte[] Dados { get; }

        protected List<T> Lista;

        protected BaseImportar(byte[] dados)
        {
            Dados = dados ?? throw new ArgumentNullException(nameof(dados));
        }

        protected abstract Task<List<T>> ConverterBytesEmDados();

        public async Task<List<T>> CriarDados()
        {
            if (Dados == null || Dados.Length == 0)
                return [];

            return await ConverterBytesEmDados();
        }
    }
}
