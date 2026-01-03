using Dapper;
using Livraria.Domain.Entities.Livro;
using Livraria.Domain.Interfaces.Repositories.Livro;
using Livraria.Infrastructure.Interfaces;
using Livraria.Infrastructure.Repositories.Base;
using System.Data;
using System.Text;

namespace Livraria.Infrastructure.Repositories.LivroRepository
{
    public class LivroWriteRepository : BaseWrite<LivroEntity>, ILivroWriteRepository
    {
        public LivroWriteRepository(IDatabaseConnection dbConnection) : base(dbConnection) { }

        public async Task<bool> Insert(LivroEntity livro, string usuarioLogado)
        {
            using var transaction = BeginTransaction();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("SELECT [id],");
                sb.AppendLine("            [qt_estoque]");
                sb.AppendLine("FROM [dbo].[Livro]");
                sb.AppendLine("WHERE [titulo] = @Titulo");
                sb.AppendLine("AND [isbn] = @Isbn;");

                var livroBanco = await Connection.QueryFirstOrDefaultAsync<(int id, int qt_estoque)>(sb.ToString(), new { livro.Titulo, livro.Isbn }, transaction);

                int idLivro;
                if (livroBanco.id == 0) //SE O SELECT NÃO RETORNAR UM ID FAZER A INSERÇÃO NO BANCO
                {
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendLine("INSERT INTO [dbo].[Livro] ([titulo], [subtitulo], [isbn], [dt_publicacao], [preco], [qt_estoque], [fk_autor], [fk_categoria])");
                    sb2.AppendLine("                           VALUES (@Titulo, @Subtitulo, @Isbn, @Dt_Publicacao, @Preco, @Qt_Estoque, @Fk_Autor, @Fk_Categoria);");

                    //PEGA O ÚLITMO ID GERADO EM UMA MESMA SESSÃO E MESMO ESCOPO APÓS UM INSERT NUMA TABELA
                    sb2.AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT);");

                    var param = new
                    {
                        livro.Titulo,
                        livro.Subtitulo,
                        livro.Isbn,
                        livro.Dt_Publicacao,
                        livro.Preco,
                        livro.Qt_Estoque,
                        livro.Fk_Autor,
                        livro.Fk_Categoria
                    };

                    idLivro = await Connection.QuerySingleAsync<int>(sb2.ToString(), param, transaction);
                }
                else //SE O SELECT RETORNAR UM ID FAZER A ATUALIZAÇÃO DO ESTOQUE
                {
                    idLivro = livroBanco.id;
                    await AtualizarEstoqueLivro(livro.Qt_Estoque, idLivro, transaction);
                }

                var movimentacao = await InserirMovimentacao(livro.Qt_Estoque, idLivro, usuarioLogado, transaction);

                if (!movimentacao)
                {
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception(ex.Message);
            }
        }

        private async Task AtualizarEstoqueLivro(int qt_estoque, int idLivro, IDbTransaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE [dbo].[Livro]");
            sb.AppendLine("SET [qt_estoque] = [qt_estoque] + @Qt_Estoque");
            sb.AppendLine("WHERE [id] = @Id;");

            await Connection.ExecuteAsync(sb.ToString(), new { qt_estoque, Id = idLivro }, transaction);
        }

        private async Task<bool> InserirMovimentacao(int qt_estoque, int idLivro, string usuarioLogado, IDbTransaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO [dbo].[Movimentacao] ([tipo], [dt_movimentacao], [quantidade], [fk_origem], [fk_livro], [fk_usuario])");
            sb.AppendLine("                                         VALUES ('ENTRADA', GETDATE(), @Quantidade, 4, @FK_Livro, @Fk_Usuario)");

            var param = new
            {
                Quantidade = qt_estoque,
                FK_Livro = idLivro,
                FK_Usuario = usuarioLogado
            };

            return await Connection.ExecuteAsync(sb.ToString(), param, transaction) > 0;
        }
    }
}
