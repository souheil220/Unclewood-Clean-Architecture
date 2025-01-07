using System.Data;
using Npgsql;
using UnclewoodCleanArchitecture.Application.Data;

namespace UnclewoodCleanArchitecture.Infrastructure.Data;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new NpgsqlConnection(_connectionString);
        connection.Open();
        return connection;
    }
}