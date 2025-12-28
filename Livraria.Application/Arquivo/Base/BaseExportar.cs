namespace Livraria.Application.Arquivo.Base
{
    public abstract class BaseExportar<T>
    {
        protected List<T> Dados { get; }

        protected BaseExportar(List<T> dados)
        {
            Dados = dados ?? throw new ArgumentNullException(nameof(dados));
        }

        protected abstract byte[] FormatarDadosEmBytes();

        public byte[] CriarBytes()
        {
            if (Dados == null || Dados.Count == 0)
                return Array.Empty<byte>();

            return FormatarDadosEmBytes();
        }
    }
}
