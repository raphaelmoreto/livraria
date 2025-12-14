using Dapper.Contrib.Extensions;
using Livraria.Domain.Entities.Base;
using Livraria.Domain.Validations;

namespace Livraria.Domain.Entities.Usuario
{
    [Table("[dbo].[Usuario]")]
    public class UsuarioEntity : BaseEntity
    {
        public string Nome { get; private set; }

        public string Usuario { get; private set; }

        public string Email { get; private set; }

        public string Senha { get; private set; }

        public UsuarioEntity() { }

        public UsuarioEntity(string nome, string usuario, string email, string senha)
        {
            AtribuirNome(nome);
            AtribuirUsuario(usuario);
            AtribuirEmail(email);
            AtribuirSenha(senha);
            Validar();
        }

        public void AtribuirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                DomainValidationException.AtribuirExcecao("NOME OBRIGATÓRIO");
                return;
            }

            if (nome == Nome)
                return;

            Nome = nome.ToUpper();
        }

        public void AtribuirUsuario(string usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario))
            {
                DomainValidationException.AtribuirExcecao("NOME DE USUÁRIO OBRIGATÓRIO");
                return;
            }

            if (usuario == Usuario)
                return;

            Usuario = usuario.ToLower();
        }

        public void AtribuirEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                DomainValidationException.AtribuirExcecao("EMAIL OBRIGATÓRIO");
                return;
            }

            if (email == Email)
                return;

            Email = email;
        }

        public void AtribuirSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                DomainValidationException.AtribuirExcecao("SENHA OBRIGATÓRIO");
                return;
            }

            if (senha == Senha)
                return;

            Senha = senha;
        }

        public override void Validar()
        {
            if (DomainValidationException.TemExcecoes())
                throw new AggregateException(DomainValidationException.Excecoes);
        }
    }
}
