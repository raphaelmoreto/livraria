
namespace Livraria.Domain.Dtos.Login
{
    public record class LoginInputDto(string Usuario, string Senha);

    public record class LoginOutputDto(int Id, string Tipo);
}
