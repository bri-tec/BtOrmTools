using Microsoft.Data.SqlClient;

namespace BtOrmCore
{
    public interface IDatabaseConnection
    {
        string? ConnectionString { get; }
        string DatabaseName { get; }
        string DatabasePassword { get; }
        string DatabaseUsername { get; }
        string ServerAddress { get; }

        SqlConnection GetSqlConnection();
        bool VerifyConnectionInfo();
    }
}