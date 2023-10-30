using Microsoft.Data.SqlClient;

namespace BtOrmCore
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public enum DatabaseType { MSSQL, mySQL, MongoDB, Other }

        public string ServerAddress { get; private set; }

        public string DatabaseName { get; private set; }

        public string DatabasePassword { get; private set; }

        public string DatabaseUsername { get; private set; }

        public string? ConnectionString { get; private set; }

        public DatabaseConnection(string serverAddress, string databaseName, string databaseUsername, string databasePassword)
        {
            ServerAddress = serverAddress;
            DatabaseName = databaseName;
            DatabaseUsername = databaseUsername;
            DatabasePassword = databasePassword;
        }

        private static SqlConnection? _SqlConnection;

        public SqlConnection GetSqlConnection()
        {
            if (ServerAddress.Length == 0)
                throw new Exception("ServerAddress is not a valid address");
            if (DatabaseName.Length == 0)
                throw new Exception("DatabaseName is not a valid database name");

            if (_SqlConnection == null)
            {
                SqlConnectionStringBuilder sqlConnBuilder = new SqlConnectionStringBuilder();
                sqlConnBuilder.DataSource = ServerAddress;
                sqlConnBuilder.InitialCatalog = DatabaseName;
                sqlConnBuilder.UserID = DatabaseUsername;
                sqlConnBuilder.Password = DatabasePassword;
                sqlConnBuilder.TrustServerCertificate = true;

                _SqlConnection = new SqlConnection(sqlConnBuilder.ConnectionString);
                ConnectionString = sqlConnBuilder.ConnectionString;
            }

            return _SqlConnection;
        }

        public bool VerifyConnectionInfo()
        {
            SqlConnection connection = GetSqlConnection();
            try
            {
                connection.Open();
                connection.Close();
                return true;
            }
            catch 
            {
                return false;
            }

        }

    }
}