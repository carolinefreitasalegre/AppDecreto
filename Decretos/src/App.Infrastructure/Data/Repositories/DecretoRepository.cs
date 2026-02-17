using App.Application.Interfaces.Repository;
using App.Domain;
using Dapper;

namespace App.Infrastructure.Data.Repositories;

public class DecretoRepository : IDecretoRepository
{
    private readonly DbConnectionFactory _connection;

    public DecretoRepository(DbConnectionFactory connection)
    {
        _connection = connection;
    }
    
    public async Task<Decreto> BuscarViaNumero(int numero)
    {
        var query = @"SELECT * FROM ""Decretos"" WHERE ""NumeroDecreto"" = @numero";

        using (var connection = _connection.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Decreto>(query, new { numero });
        }
    }

    public async Task<Decreto> BuscarViaId(int id)
    {
        var query = @"SELECT * FROM ""Decretos"" WHERE ""Id"" = @id";

        using (var connection = _connection.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Decreto>(query, new { id });
        }
    }

    public async Task<List<Decreto>> ListarDecretos()
    {
        var query = @"SELECT * FROM ""Decretos""";
        

        using (var connection = _connection.CreateConnection())
        {
            var usuarios = await connection.QueryAsync<Decreto>(query);
            return usuarios.ToList();
        }
    }

    public async Task<Decreto> AdicionarDecreto(Decreto decreto)
    {
        var query = @"INSERT INTO ""Decretos"" 
                (""NumeroDecreto"", ""Solicitante"", ""DataSolicitacao"", ""DataParaUso"", ""Secretaria"", ""Justificativa"", ""UsuarioId"") 
              VALUES
                (@NumeroDecreto, @Solicitante, @DataSolicitacao, @DataParaUso, @Secretaria::text::secretaria_enum, @Justificativa, @UsuarioId)
              RETURNING *;";

        using (var connection = _connection.CreateConnection())
        {
            return await connection.QuerySingleAsync<Decreto>(query, new
            {
                decreto.NumeroDecreto,
                decreto.Solicitante,
                decreto.DataSolicitacao,
                decreto.DataParaUso,
                Secretaria = decreto.Secretaria.ToString(),
                decreto.Justificativa,
                decreto.UsuarioId
            });
        }
    }

    public async Task<Decreto> EditarDecreto(Decreto decreto, int id)
    {
        var query = @"UPDATE ""Decretos"" SET 
            ""NumeroDecreto"" = @NumeroDecreto, 
            ""Solicitante"" = @Solicitante, 
            ""DataSolicitacao"" = @DataSolicitacao, 
            ""DataParaUso"" = @DataParaUso, 
            ""Secretaria"" = @Secretaria::text::secretaria_enum, 
            ""Justificativa"" = @Justificativa, 
            ""UsuarioId"" = @UsuarioId  
            WHERE ""Id"" = @Id RETURNING *"; 

        using var connection = _connection.CreateConnection();
    
        return await connection.QuerySingleAsync<Decreto>(query, new
        {
            decreto.NumeroDecreto,
            decreto.Solicitante,
            decreto.DataSolicitacao,
            decreto.DataParaUso,
            Secretaria = decreto.Secretaria.ToString(),
            decreto.Justificativa,
        });
    }
}