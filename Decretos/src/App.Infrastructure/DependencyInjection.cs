using App.Domain.Enums;
using App.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace App.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        NpgsqlConnection.GlobalTypeMapper.MapEnum<Secretaria>();

        services.AddScoped<DbConnectionFactory>();

        return services;
    }
}