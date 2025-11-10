
namespace Livraria.Domain.Validations
{
    public class DomainValidationException : Exception
    {
        public static List<DomainValidationException> Excecoes = [];

        public DomainValidationException(string mensagem) : base(mensagem) { }

        public static void AtribuirExcecao(string mensagem)
        {
            Excecoes.Add(new DomainValidationException(mensagem));
        }

        public static bool TemExcecoes() => Excecoes.Any();
    }
}
