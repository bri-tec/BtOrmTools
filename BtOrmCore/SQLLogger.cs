using Loggers;
using Microsoft.Extensions.Logging;

namespace BtOrmCore
{
    public class SQLLogger : Loggers.ILogger, Microsoft.Extensions.Logging.ILogger
    {
        private IDatabaseConnection _databaseConnection;

        public SQLLogger(IDatabaseConnection databaseConnection)
        {
            _databaseConnection = databaseConnection;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public bool Log(DateTime logDateTime, string message, object source)
        {
            string sql = $"INSERT INTO Logs ([LogDateTime],[Message],[LogSource]) VALUES ('{logDateTime.ToUniversalTime().ToString()}','{message.Replace("'","''")}','{source.GetType().Name.Replace("'","''")}')";
            int result = BasicSQLFunctions.ExecuteNonQuery(_databaseConnection.GetSqlConnection(), sql);

            return result == 1 ? true : false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (typeof(TState) == typeof(string))
            {
                if (!Log(DateTime.Now, state as string, "Logger"))
                {
                    throw new Exception("Failed to log to database.");
                }
            }
           
        }
    }
}
