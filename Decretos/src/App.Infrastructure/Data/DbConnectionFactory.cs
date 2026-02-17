using Npgsql;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace App.Infrastructure.Data;

public class DbConnectionFactory
{
    
    private readonly string _connectionString;

    public DbConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection") 
                ?? throw new InvalidOperationException("Connection string 'connection' não encontrada.");
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}