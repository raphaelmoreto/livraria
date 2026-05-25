using Dapper.Contrib.Extensions;
using Flunt.Notifications;
using Flunt.Validations;
using Livraria.Domain.Entities.Base;

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
        }

        public void AtribuirNome(string nome)
        {
            if (nome == Nome)
                return;

            Nome = nome?.Trim().ToUpper() ?? string.Empty;
        }

        public void AtribuirUsuario(string usuario)
        {
            if (usuario == Usuario)
                return;

            Usuario = usuario?.Trim().ToLower() ?? string.Empty;
        }

        public void AtribuirEmail(string email)
        {
            if (email.ToLower() == Email)
                return;

            Email = email?.Trim().ToLower() ?? string.Empty;
        }

        public void AtribuirSenha(string senha)
        {
            if (senha == Senha)
                return;

            Senha = senha?.Trim() ?? string.Empty;
        }

        public void AtribuirPerfil(int fk_Perfil)
        {
            if (fk_Perfil == Fk_Perfil)
                return;

            Fk_Perfil = fk_Perfil;
        }

        public override bool Validar()
        {
            Clear();

            AddNotifications(new Contract<Notification>()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, "Nome", "NOME DE USUÁRIO OBRIGATÓRIO")
                .IsNotNullOrWhiteSpace(Usuario, "Usuario", "USUÁRIO OBRIGATÓRIO")
                .IsNotNullOrWhiteSpace(Email, "Email", "EMAIL OBRIGATÓRIO")
                .IsNotNullOrWhiteSpace(Senha, "Senha", "SENHA OBRIGATÓRIA")
                .IsGreaterThan(Fk_Perfil, 0, "Perfil", "PERFIL DE USUÁRIO NÃO DECLARADO")
            );

            return IsValid;
        }
    }
}
