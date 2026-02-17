using App.Application.Interfaces.Repository;
using App.Domain;
using Dapper;

namespace App.Infrastructure.Data.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly DbConnectionFactory _dbConnectionFactory;

    public UsuarioRepository(DbConnectionFactory dbConnectionFactory)
    {
        _dbConnectionFactory = dbConnectionFactory;
    }

    public async Task<Usuario> BuscarViaEmail(string email)
    {
        var query = @"SELECT * FROM ""Usuarios"" WHERE ""Email"" = @email";

        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new {email});
        }
    }

    public async Task<Usuario> BuscarViaId(int id)
    {
        var query = @"SELECT * FROM ""Usuarios"" WHERE ""Id"" = @id";

        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { id });
        }
    }

    public async Task<Usuario> BuscarViaMatricula(int matricula)
    {
        var query = @"SELECT * FROM ""Usuarios"" WHERE ""Matricula"" = @matricula";
        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            return await connection.QueryFirstOrDefaultAsync<Usuario>(query, new { matricula });
        }
    }

    public async Task<List<Usuario>> ListarUsuarios()
    {
        var query = @"SELECT * FROM ""Usuarios""";
        using (var connection = _dbConnectionFactory.CreateConnection())
        {
            var usuarios = await connection.QueryAsync<Usuario>(query);
            return usuarios.ToList();
        }
    }

    public async Task<Usuario> AdicionarUsuario(Usuario usuario)
    {
        var query = @"INSERT INTO ""Usuarios"" (""Matricula"" , ""Nome"", ""Email"", ""Senha"", ""Role"", ""Status"", ""CriadoEm"") 
                        VALUES(@Matricula , @Nome, @Email, @Senha, @Role, @Status, @CriadoEm)
                        RETURNING *;";

        using (var connection = _dbConnectionFactory.CreateConnection())
        {
           return await connection.QuerySingleAsync<Usuario>(query, new
           {
               usuario.Matricula,
               usuario.Nome,
               usuario.Email,
               usuario.Senha,
               usuario.Role,
               usuario.Status
           });
        }
    }

    public async Task<Usuario> EditarUsuario(Usuario usuario)
    {
        var id = usuario.Id;
        var query = @"UPDATE ""Usuarios"" SET ""Matricula""=@Matricula, ""Nome""=@Nome, ""Email""=@Email, ""Senha""=@Senha, 
                        ""Role""=@Role, ""Status""=@Status
                    WHERE ""Id"" = @id";
        
        using var connection = _dbConnectionFactory.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Usuario>(query, usuario);
    }
}