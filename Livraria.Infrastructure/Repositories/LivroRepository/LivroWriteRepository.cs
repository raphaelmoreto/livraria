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

        //public async Task<bool> BuscarLivroPorNome(string nomeLivro, string isbn)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.AppendLine("SELECT COUNT(CASE WHEN [titulo] = @Titulo");
        //    sb.AppendLine("                        AND [isbn] = @Isbn");
        //    sb.AppendLine("                        THEN 1 END)");
        //    sb.AppendLine("FROM [dbo].[Livro]");

        //    return await Connection.QuerySingleAsync(sb.ToString(), new { Titulo = nomeLivro, Isbn = isbn});
        //}

        public override async Task<bool> Insert(LivroEntity livro)
        {
            using var transaction = BeginTransaction();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO [dbo].[Livro] ([titulo], [subtitulo], [isbn], [dt_publicacao], [preco], [qt_estoque], [fk_autor], [fk_categoria])");
                sb.AppendLine("SELECT @Titulo,");
                sb.AppendLine("            @Subtitulo,");
                sb.AppendLine("            @Isbn,");
                sb.AppendLine("            @Dt_Publicacao,");
                sb.AppendLine("            @Preco,");
                sb.AppendLine("            @Qt_Estoque,");
                sb.AppendLine("            @Fk_Autor,");
                sb.AppendLine("            @Fk_Categoria");
                sb.AppendLine("WHERE NOT EXISTS (");
                sb.AppendLine("         SELECT [Id]");
                sb.AppendLine("         FROM [dbo].[Livro] AS livro");
                sb.AppendLine("         WHERE livro.titulo = @Titulo");
                sb.AppendLine("         AND livro.isbn = @Isbn");
                sb.AppendLine(")");

                //PEGA O ÚLITMO ID GERADO EM UMA MESMA SESSÃO E MESMO ESCOPO APÓS UM INSERT NUMA TABELA
                sb.AppendLine("SELECT CAST(SCOPE_IDENTITY() AS INT)");

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

                var idLivro = await Connection.QuerySingleAsync<int>(sb.ToString(), param, transaction);

                if (idLivro > 0)
                {
                    StringBuilder sb2 = new StringBuilder();
                    sb2.AppendLine("INSERT INTO [dbo].[Movimentacao] ([tipo], [dt_movimentacao], [quantidade], [fk_livro])");
                    sb2.AppendLine("                                         VALUES ('ENTRADA', GETDATE(), @Quantidade, @FK_Livro)");

                    var movimentacao = await Connection.ExecuteAsync(sb2.ToString(), new { Quantidade = livro.Qt_Estoque, FK_Livro = idLivro }, transaction) > 0;

                    if (!movimentacao)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                else
                {
                    transaction.Rollback();
                    return false;
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
