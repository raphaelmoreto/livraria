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

        public int Fk_Perfil { get; private set; }

        public UsuarioEntity() { }

        public UsuarioEntity(string nome, string usuario, string email, string senha, int fk_Perfil)
        {
            AtribuirNome(nome);
            AtribuirUsuario(usuario);
            AtribuirEmail(email);
            AtribuirSenha(senha);
            AtribuirPerfil(fk_Perfil);
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

            if (email.ToLower() == Email)
                return;

            Email = email.ToLower();
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

        public void AtribuirPerfil(int fk_Perfil)
        {
            if (fk_Perfil == 0)
            {
                DomainValidationException.AtribuirExcecao("PERFIL DE USUÁRIO NÃO DECLARADO");
                return;
            }

            if (fk_Perfil == Fk_Perfil)
                return;

            Fk_Perfil = fk_Perfil;
        }

        public override void Validar()
        {
            if (DomainValidationException.TemExcecoes())
                throw new AggregateException(DomainValidationException.Excecoes);
        }
    }
}
