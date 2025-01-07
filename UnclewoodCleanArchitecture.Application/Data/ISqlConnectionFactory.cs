using System.Data;

namespace UnclewoodCleanArchitecture.Application.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}